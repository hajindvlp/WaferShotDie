using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

        public static draw WaferDraw = new draw(0, 0);
        public ScaleTransform WaferSt = new ScaleTransform(1, -1);
        public TranslateTransform WaferTt = new TranslateTransform();
        public TransformGroup WaferTg = new TransformGroup();

        public static Logger logger;

        protected bool isDragging;
        private Point clickPosition;
        public double baseX, baseY, aX, aY;

        public MainWindow()
        {
            InitializeComponent();

            logger = new Logger();

            //logger.Show();

            WaferTg.Children.Add(WaferSt);
            WaferTg.Children.Add(WaferTt);
            WaferCanvas.RenderTransform = WaferTg;
            WaferDraw.cv = WaferCanvas;
        }

        public static void DrawFullWafer()
        {
            int i, j, k;
            int maxFitUnitOnWidth = (int)Math.Ceiling(Chips.Wafer.Radius / Chips.Unit.width) + 1;
            int maxFitUnitOnHeight = (int)Math.Ceiling(Chips.Wafer.Radius / Chips.Unit.height) + 1;
            double startY, startX, UnitStartY, UnitStartX;

            logger.Log($"{maxFitUnitOnHeight} {maxFitUnitOnWidth}");

            startY = Chips.whenMxChip.y;
            startX = Chips.whenMxChip.x;
            for (i = -maxFitUnitOnHeight; i <= maxFitUnitOnHeight; i++)
            {
                for (j = -maxFitUnitOnWidth; j <= maxFitUnitOnWidth; j++)
                {
                    UnitStartY = i * Chips.Unit.height + startY;
                    UnitStartX = j * Chips.Unit.width + startX;

                    logger.Log($"{UnitStartX} {UnitStartY}");

                    bool UnitOk = false;
                    for (k = 0; k < 4; k++)
                    {
                        double dist = Chips.distToO(UnitStartX + Chips.Unit.coord[k].x, UnitStartY + Chips.Unit.coord[k].y);

                        if (dist < Chips.Wafer.Radius) UnitOk = true;
                    }

                    if (UnitOk)
                    {
                        WaferDraw.rect(UnitStartX, UnitStartY, Chips.Unit.width, Chips.Unit.height, "#001580", 0.3);

                        foreach (_Chips._Chip chip in Chips.Unit.set)
                        {
                            if (!chip.Disabled) WaferDraw.rect(UnitStartX + chip.startX, UnitStartY + chip.startY,
                                                               chip.width, chip.height,
                                                               "#7600c9", 0.3);
                        }
                    }
                }
            }
        }

        public static void DrawWafer(bool Final)
        {
            WaferDraw.clearCv();
            WaferDraw.circle(0, 0, Chips.Wafer.Diameter, "#de9726", 0.3);
            
            if (Final) DrawFullWafer();
            else
            {
                WaferDraw.rect(0, 0, Chips.Unit.width, Chips.Unit.height, "#001580", 0.3);
                foreach (_Chips._Chip chip in Chips.Unit.set)
                {
                    if (!chip.Disabled) WaferDraw.rect(chip.startX, chip.startY, chip.width, chip.height, "#7600c9", 0.3);
                }
            }
            
        }

        private void numUnitWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            Chips.Unit.width = ToDouble(numUnitWidth.Text);
            DrawWafer(false);
        }

        private void numUnitHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            Chips.Unit.height = ToDouble(numUnitHeight.Text);
            DrawWafer(false);
        }

        private void numWaferDiameter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Chips.Wafer.Diameter = ToDouble(numWaferDiameter.Text);
            Chips.Wafer.Radius = Chips.Wafer.Diameter / 2;
            DrawWafer(false);
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
            DrawWafer(false);
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            Chips.Calculate();

            logger.Log($"{Chips.mxChipCnt} {Chips.mxUnitCnt} {Chips.mxDist}");

            mxUnitNumText.Text = Chips.mxUnitCnt.ToString();
            mxChipNumText.Text = Chips.mxChipCnt.ToString();
            mxDistText.Text = Chips.mxDist.ToString();

            DrawWafer(true);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            logger.Close();
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

        private void WaferCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                logger.Log("dsfads");
                WaferTt.X = WaferTt.Y = 0;
                WaferSt.ScaleX = 1; 
                WaferSt.ScaleY = -1;
                return;
            }

            isDragging = true;
            clickPosition = e.GetPosition(WaferCanvasContainer);
            WaferCanvasContainer.CaptureMouse();

            aX = 0;
            aY = 0;
        }

        private void WaferCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            clickPosition = e.GetPosition(WaferCanvasContainer);
            WaferCanvasContainer.ReleaseMouseCapture();

            baseX = WaferTt.X;
            baseY = WaferTt.Y;

            aX = 0;
            aY = 0;
        }

        private void WaferCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && sender != null) 
            {
                Point currentPosition = e.GetPosition(WaferCanvasContainer);
                aX = WaferTt.X = baseX + currentPosition.X - clickPosition.X;
                aY = WaferTt.Y = baseY + currentPosition.Y - clickPosition.Y;
            } 
        }
    }
                    
    public class _Chips
    {
        public _Wafer Wafer = new _Wafer();
        public _Unit Unit = new _Unit();

        public int mxUnitCnt, mxChipCnt;
        public _coord whenMxUnit, whenMxChip;
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
            public int ChipNum, ActualChipNum;
            public List<_Chip> set = new List<_Chip>();
            public _coord[] coord = new _coord[4];
        }

        public class _Chip
        {
            public bool Disabled = false;
            public double width, height;
            public double startX, startY;
            public _coord[] coord = new _coord[4];

            public void clear()
            {
                Disabled = true;
                width = height = startX = startY = 0;
            }
        }

        public void Calculate()
        {
            double i, j;
            int k, l, m, n;

            List<_Chip> actualSet = new List<_Chip>();
            int ChipNum = 0;

            mxChipCnt = mxUnitCnt = 0;
            mxDist = 0;

            Unit.coord[0] = new _coord(0, 0);
            Unit.coord[1] = new _coord(Unit.width, 0);
            Unit.coord[2] = new _coord(0, Unit.height);
            Unit.coord[3] = new _coord(Unit.width, Unit.height);

            for(k=0; k<Unit.ChipNum; k++)
            {
                if (!Unit.set[k].Disabled) 
                {
                    Unit.ActualChipNum++;
                    ChipNum++;
                    actualSet.Add(Unit.set[k]);

                    actualSet[ChipNum-1].coord[0] = new _coord(actualSet[ChipNum-1].startX, actualSet[ChipNum-1].startY);
                    actualSet[ChipNum-1].coord[1] = new _coord(actualSet[ChipNum-1].startX + actualSet[ChipNum-1].width, actualSet[ChipNum-1].startY);
                    actualSet[ChipNum-1].coord[2] = new _coord(actualSet[ChipNum-1].startX, actualSet[ChipNum-1].startY + actualSet[ChipNum-1].height);
                    actualSet[ChipNum-1].coord[3] = new _coord(actualSet[ChipNum-1].startX + actualSet[ChipNum-1].width, actualSet[ChipNum-1].startY + actualSet[ChipNum-1].height);
                }
            }

            int maxFitUnitOnWidth = (int)Math.Ceiling(Wafer.Radius / Unit.width) + 1;
            int maxFitUnitOnHeight = (int)Math.Ceiling(Wafer.Radius / Unit.height) + 1;

            double UnitStartX, UnitStartY;
            double stepX, stepY;
            int UnitCnt, ChipCnt;

            stepX = stepY = 0.5;

            for (i=-Unit.height; i<=Unit.height; i+=stepY)
                for(j=-Unit.width; j<=Unit.width; j+=stepX)
                {
                    UnitCnt = ChipCnt = 0;

                    for(k=-maxFitUnitOnHeight; k<=maxFitUnitOnHeight; k++)
                        for(l=-maxFitUnitOnWidth; l<=maxFitUnitOnWidth; l++)
                        {
                            UnitStartY = k * Unit.height + i;
                            UnitStartX = l * Unit.width + j;

                            bool UnitOk = true;
                            double tmxDist = 0;
                            for (n = 0; n < 4; n++)
                            {
                                double dist = distToO(UnitStartX + Unit.coord[n].x, UnitStartY + Unit.coord[n].y);

                                if (dist > Wafer.Radius) UnitOk = false;
                                if (dist > tmxDist) tmxDist = dist;
                            }

                            if (UnitOk) {
                                UnitCnt++;
                                ChipCnt += ChipNum;

                                if (tmxDist > mxDist) mxDist = tmxDist;
                                continue;
                            }

                            for (m=0; m<ChipNum; m++)
                            {
                                bool ChipOk = true;
                                tmxDist = 0;
                                for (n=0; n<4; n++)
                                {
                                    double dist = distToO(UnitStartX + actualSet[m].coord[n].x, UnitStartY + actualSet[m].coord[n].y);

                                    if (dist > tmxDist) tmxDist = dist;
                                    if (dist > Wafer.Radius) ChipOk = false;
                                }

                                if (ChipOk)
                                {
                                    ChipCnt++;
                                    if (tmxDist > mxDist) mxDist = tmxDist;
                                }
                            }
                        }

                    if (mxUnitCnt < UnitCnt) 
                    {
                        mxUnitCnt = UnitCnt;
                        whenMxChip = new _coord(j, i);
                        MainWindow.logger.Log($"mxUnitCnt Updated : {mxUnitCnt} When {i} {j} {ChipCnt}");
                    }
                    if (mxChipCnt < ChipCnt) 
                    {
                        mxChipCnt = ChipCnt;
                        whenMxChip = new _coord(j, i);
                        MainWindow.logger.Log($"mxChipCnt Updated : {mxChipCnt} When {i} {j} {UnitCnt}");
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
                SolidColorBrush FillBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorHex));
                FillBrush.Opacity = opacity;

                Ellipse circle = new Ellipse()
                {
                    Width = diameter * scale,
                    Height = diameter * scale,
                    Fill = FillBrush,
                    Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorHex)),
                    StrokeThickness = 0.5
                };

                circle.SetValue(Canvas.LeftProperty, x * scale);
                circle.SetValue(Canvas.TopProperty, y * scale);

                cv.Children.Add(circle);
            }

            private void drawRect()
            {
                SolidColorBrush FillBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorHex));
                FillBrush.Opacity = opacity;

                Rectangle rect = new Rectangle()
                {
                    Width = width * scale,
                    Height = height * scale,
                    Fill = FillBrush,
                    Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorHex)),
                    StrokeThickness = 0.5,
                };

                rect.SetValue(Canvas.LeftProperty, x * scale);
                rect.SetValue(Canvas.TopProperty, y * scale);

                cv.Children.Add(rect);
            }
        }
    }
}
