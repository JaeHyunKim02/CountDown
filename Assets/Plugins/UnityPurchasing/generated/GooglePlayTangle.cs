#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("T2rQk5h3wcmWfqHR3DZW9ohIQrhnp8WinsTocYNkZCqHUqNIL7ZAAEKNrRgHYL0tCa77YrMOmzWwA4lgYE+DW99Y4fgGV7RLgjh686UKD2XPLjauvOUjQWSbiWtRzhihEtk5KPpabLaRB21hBtyPR1C1NnKqAyQH8MbHj7nEx0+of4LHZOSPHdvtk9Yt2c4XMYx4alad4UgtQIrhhce9y2PRUnFjXlVaedUb1aReUlJSVlNQn7ZO6O4pyCWbCR/Tee1inSwluaubMPPu3McX9AlT66EFpqfWyVsa+x2ytR61lwRTz6X5WHRkN7ffEt8F0VJcU2PRUllR0VJSU+KsDydl4QSdtsjUHh+P6FQdTK4owUuy6Kp9/nsnOpYgYeIRPFFQUlNS");
        private static int[] order = new int[] { 10,11,2,6,7,8,8,8,10,13,12,12,12,13,14 };
        private static int key = 83;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
