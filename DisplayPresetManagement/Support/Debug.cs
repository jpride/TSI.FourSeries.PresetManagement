namespace TSI.FourSeries.DisplayPresetManagement
{
    class Debug
    {
        public static bool debugEnable = false;


        /// <summary>
        /// Sets debugEnable using '0' for false and '1' for true
        /// </summary>
        /// <param name="flag"></param>
        public static void SetDebug(ushort flag)
        {
            debugEnable = (flag == 1) ? true : false;
        }

        /// <summary>
        /// Gets the current value of debugEnable, returning 0 (false) or 1 (true)
        /// </summary>
        /// <returns></returns>
        public static ushort GetDebug()
        {
            return (ushort)(debugEnable ? 1 : 0);
        }
    }
}