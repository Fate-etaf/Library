using System.Windows;

namespace LibraryBorrowBook
{
    public static class WindowExtensions
    {
        public static void SwitchTo(this Window current, Window next)
        {
            // Inherit WindowState (Normal, Minimized, Maximized)
            next.WindowState = current.WindowState;

            // Inherit size and position if it's Normal window
            if (current.WindowState == WindowState.Normal)
            {
                next.WindowStartupLocation = WindowStartupLocation.Manual;
                next.Top = current.Top;
                next.Left = current.Left;
                next.Width = current.Width;
                next.Height = current.Height;
            }

            next.Show();
            current.Close();
        }
    }
}
