using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace RayTrace4D
{

    class RaytraceEngine
    {
        private Thread GameLoopThread;
        private Canvas canvas = null;
        private int frameCount = 0;

        private Vector5 eye;
        private Vector5 up;
        private Vector5 right;
        private Vector5 forward;

        private int sizeX = 512, sizeY = 512;

        List<IRaytraceObject> objectBuffer = new List<IRaytraceObject>();

        public RaytraceEngine()
        {
            canvas = new Canvas();
            canvas.Size = new Size(sizeX, sizeY);
            canvas.Text = "RayTrace";
            canvas.Paint += Renderer;
            OnLoad();

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(canvas);
        }

        private void OnLoad()
        {
            eye = new Vector5(0, 0, 0, 0, 1);
            up = new Vector5(0, 1, 0, 0, 0);
            right = new Vector5(1, 0, 0, 0, 0);
            forward = new Vector5(0, 0, -1, 0, 0);
        }

        void GameLoop()
        {
            while (GameLoopThread.IsAlive)
            {
                frameCount++;
                try
                {
                    canvas.BeginInvoke((MethodInvoker)delegate { canvas.Refresh(); });
                    Thread.Sleep(1);
                }
                catch { }
            }
        }

        private Pen pen = new Pen(Brushes.Black);

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Aqua);

            double maxDim = Math.Max(sizeX, sizeY);
            Ray r = new Ray();
            RayResult result = new RayResult();
            for (int x = 0; x < sizeX; x++)
            {
                for(int y = 0; y < sizeY; y++)
                {
                    double sx = (2*x-sizeX)/ maxDim;
                    double sy = (sizeY - 2 * y) / maxDim;
                    
                    r.origin = eye;
                    r.direction = forward + sx * right + sy * up;

                    shootRay(in r, ref result);
                    if(result.didHit)
                    {
                        pen.Color = result.color.toColor();
                        g.DrawLine(pen, x, y, x, y);
                    }
                    
                }
            }

            g.DrawLine(pen, 128, 128, 128 + (int)(20 * Math.Sin(frameCount / 10d)), 128 + (int)(20 * Math.Cos(frameCount / 10d)));
        }

        private void shootRay(in Ray r, ref RayResult result)
        {
            double nearestDist = double.MaxValue;
            IRaytraceObject nearestObj = null;

            foreach (IRaytraceObject obj in objectBuffer)
            {
                double dist = obj.intersect(r);
                if(dist < nearestDist)
                {
                    nearestDist = dist;
                    nearestObj = obj;
                }
            }

            result.didHit = nearestObj != null;
            if(nearestObj != null)
            {
                Vector5 p = r.origin + r.direction * nearestDist;
                result.color = nearestObj.getColor(p);
            }
        }

        
    }
}
