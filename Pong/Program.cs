namespace Pong
{
    using OpenGL_Helper;

    class Program
    {
        static void Main(string[] args)
        {
            using (Window win = new Window("Pong"))
                win.Run();
        }
    }
}
