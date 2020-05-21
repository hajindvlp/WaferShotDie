using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WaferShotDie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static _Chips Chips = new _Chips();

        public draw WaferDraw = new draw(0, 0);
        public ScaleTransform WaferSt = new ScaleTransform(1, -1);
        public TranslateTransform WaferTt = new TranslateTransform();
        public TransformGroup WaferTg = new TransformGroup();

        public static Logger logger;

        public MainWindow()
        {
            InitializeComponent();

            logger = new Logger();
            logger.Show();

            WaferTg.Children.Add(WaferSt);
            WaferTg.Children.Add(WaferTt);
            WaferCanvas.RenderTransform = WaferTg;
            WaferDraw.cv = WaferCanvas;
        }

        private void numUnitWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            Chips.Unit.width = ToDouble(numUnitWidth.Text);
        }

        private void numUnitHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            Chips.Unit.height = ToDouble(numUnitHeight.Text);
        }

        private void numWaferDiameter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Chips.Wafer.Diameter = ToDouble(numWaferDiameter.Text);
            Chips.Wafer.Radius = Chips.Wafer.Diameter / 2;
        }

        private double ToDouble(string S)
        {
            try
            {
                return Convert.ToDouble(S);
            }
            catch
            {
                return 0f;
            }
        }

        private void AddNewChipButton_Click(object sender, RoutedEventArgs e)
        {
            Chips.Unit.ChipNum++;
            Chips.Unit.set.Add(new _Chips._Chip());

            ChipsetListView.Children.Add(new ChipConfig(Chips.Unit.ChipNum));
        }

        private void DrawChipButton_Click(object sender, RoutedEventArgs e)
        {
            WaferDraw.clearCv();
            WaferDraw.circle(0, 0, Chips.Wafer.Diameter, "#de9726", 0.3);
            WaferDraw.rect(0, 0, Chips.Unit.width, Chips.Unit.height, "#001580", 0.3);

            foreach(_Chips._Chip chip in Chips.Unit.set)
            {
                WaferDraw.rect(chip.startX, chip.startY, chip.width, chip.height, "#7600c9", 0.5);
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            Chips.Calculate();

            logger.Log($"{Chips.mxChipCnt} {Chips.mxUnitCnt} {Chips.mxDist}");
        }

        private void WaferCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                WaferSt.ScaleX *= 1.1;
                WaferSt.ScaleY *= 1.1;

                WaferSt.ScaleY = -Math.Abs(WaferSt.ScaleY);
            }
            else
            {
                WaferSt.ScaleX /= 1.1;
                WaferSt.ScaleY /= 1.1;

                WaferSt.ScaleY = -Math.Abs(WaferSt.ScaleY);
            }
        }

        protected bool isDragging;
        private Point clickPosition;

        private void WaferCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            clickPosition = e.GetPosition(WaferCanvasContainer);
            WaferCanvasContainer.CaptureMouse();
        }

        private void WaferCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            clickPosition = e.GetPosition(WaferCanvasContainer);
            WaferCanvasContainer.ReleaseMouseCapture();
        }

        private void WaferCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && sender != null) 
            {
                Point currentPosition = e.GetPosition(WaferCanvasContainer);
                WaferTt.X += currentPosition.X - clickPosition.X;
                WaferTt.Y += currentPosition.Y - clickPosition.Y;

                logger.Log($"{currentPosition.X - clickPosition.X} {currentPosition.Y - clickPosition.Y}");
            } 
        }
    }

    public class _Chips
    {
        public _Wafer Wafer = new _Wafer();
        public _Unit Unit = new _Unit();

        public int mxUnitCnt, mxChipCnt;
        public double mxDist;

        public class _coord
        {
            public double x, y;

            public _coord() { }

            public _coord(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public class _Wafer
        {
            public double Diameter;
            public double Radius;
        }

        public class _Unit
        {
            public double width, height;
            public int ChipNum;
            public List<_Chip> set = new List<_Chip>();
            public _coord[] coord = new _coord[4];
        }

        public class _Chip
        {
            public double width, height;
            public double startX, startY;
            public _coord[] coord = new _coord[4];
        }

        public void Calculate()
        {
            double i, j;
            int k, l, m, n;

            Unit.coord[0] = new _coord(0, 0);
            Unit.coord[1] = new _coord(Unit.width, 0);
            Unit.coord[2] = new _coord(0, Unit.height);
            Unit.coord[3] = new _coord(Unit.width, Unit.height);

            for(k=0; k<Unit.ChipNum; k++)
            {
                Unit.set[k].coord[0] = new _coord(Unit.set[k].startX, Unit.set[k].startY);
                Unit.set[k].coord[1] = new _coord(Unit.set[k].startX + Unit.set[k].width, Unit.set[k].startY);
                Unit.set[k].coord[2] = new _coord(Unit.set[k].startX, Unit.set[k].startY+Unit.set[k].height);
                Unit.set[k].coord[3] = new _coord(Unit.set[k].startX + Unit.set[k].width, Unit.set[k].startY + Unit.set[k].height);
            }

            int maxFitUnitOnWidth = (int)Math.Floor(Wafer.Radius / Unit.width) + 1;
            int maxFitUnitOnHeight = (int)Math.Floor(Wafer.Radius / Unit.height) + 1;

            double UnitStartX, UnitStartY;
            double stepX, stepY;
            int UnitCnt, ChipCnt;

            stepX = stepY = 0.5;

            MainWindow.logger.Log("ddd");

            for (i=-Unit.height; i<=Unit.height; i+=stepY)
                for(j=-Unit.width; j<=Unit.width; j+=stepX)
                {
                    UnitCnt = ChipCnt = 0;

                    for(k=-maxFitUnitOnHeight; k<=maxFitUnitOnHeight; k++)
                        for(l=-maxFitUnitOnWidth; l<=maxFitUnitOnWidth; l++)
                        {
                            UnitStartY = (double)k * Unit.height + i;
                            UnitStartX = (double)l * Unit.width + i;

                            bool UnitOk = true;
                            for (n = 0; n < 4; n++)
                            {
                                double dist = distToO(UnitStartX + Unit.coord[n].x, UnitStartY + Unit.coord[n].y);

                                if (dist > Wafer.Radius) UnitOk = false;
                            }

                            if (UnitOk) {
                                UnitCnt++;
                                continue;
                            }

                            for (m=0; m<Unit.ChipNum; m++)
                            {
                                bool ChipOk = true;
                                for(n=0; n<4; n++)
                                {
                                    double dist = distToO(UnitStartX + Unit.set[m].coord[n].x, UnitStartY + Unit.set[m].coord[n].y);

                                    if (dist > mxDist) mxDist = dist;

                                    if (dist > Wafer.Radius) ChipOk = false;
                                }

                                if (ChipOk) ChipCnt++;
                            }
                        }

                    if (mxUnitCnt < UnitCnt) 
                    {
                        mxUnitCnt = UnitCnt;
                        MainWindow.logger.Log($"mxUnitCnt Updated : {mxUnitCnt}");
                    }
                    if (mxChipCnt < ChipCnt) 
                    {
                        mxChipCnt = ChipCnt;
                        MainWindow.logger.Log($"mxChipCnt Updated : {mxChipCnt}");
                    }
                }
            }

        public double distToO(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }
    }

    public class draw
    {
        public Canvas cv;
        public static double scale = 1f;
        public double sx, sy;

        public List<DrawCommand> drawCommands = new List<DrawCommand>();

        public draw(double sx, double sy)
        {
            this.sx = sx;
            this.sy = sy;
        }

        public void circle(double x, double y, double diameter, string colorHex, double opacity)
        {
            drawCommands.Add(new DrawCommand(cv, DrawCommand.DRAW_CIRCLE, sx + x, sy + y, diameter, colorHex, opacity));
        }

        public void rect(double x, double y, double w, double h, string colorHex, double opacity)
        {
            drawCommands.Add(new DrawCommand(cv, DrawCommand.DRAW_RECT, sx + x, sy + y, w, h, colorHex, opacity));
        }

        public void reDraw()
        {
            clearCv();
            foreach(DrawCommand dc in drawCommands)
            {
                dc.draw();
            }
        }

        public void clearCv()
        {
            cv.Children.Clear();
        }

        public class DrawCommand
        {
            public const int DRAW_CIRCLE = 0;
            public const int DRAW_RECT = 1;

            public Canvas cv;
            public int drawType;
            public double x, y, diameter, width, height, opacity;
            public string colorHex;

            // Circle
            public DrawCommand(Canvas cv, int drawType, double x, double y, double diameter, string colorHex, double opacity)
            {
                this.cv = cv;
                this.drawType = drawType;
                this.x = x - diameter / 2;
                this.y = y - diameter / 2;
                this.diameter = diameter;
                this.colorHex = colorHex;
                this.opacity = opacity;

                drawCircle();
            }

            // Rect
            public DrawCommand(Canvas cv, int drawType, double x, double y, double width, double height, string colorHex, double opacity)
            {
                this.cv = cv;
                this.drawType = drawType;
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
                this.colorHex = colorHex;
                this.opacity = opacity;

                drawRect();
            }

            public void draw()
            {
                if (drawType == DRAW_CIRCLE) drawCircle();
                else if (drawType == DRAW_RECT) drawRect();
            }

            private void drawCircle()
            {
                Ellipse circle = new Ellipse()
                {
                    Width = diameter * scale,
                    Height = diameter * scale,
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorHex)),
                    Opacity = opacity
                };

                circle.SetValue(Canvas.LeftProperty, x * scale);
                circle.SetValue(Canvas.TopProperty, y * scale);

                cv.Children.Add(circle);
            }

            private void drawRect()
            {
                Rectangle rect = new Rectangle()
                {
                    Width = width * scale,
                    Height = height * scale,
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorHex)),
                    Opacity = opacity
                };

                rect.SetValue(Canvas.LeftProperty, x * scale);
                rect.SetValue(Canvas.TopProperty, y * scale);

                cv.Children.Add(rect);
            }
        }
    }
}
