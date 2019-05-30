using System;
namespace SpeedTest.Globals
{
    public static class JavaScriptInvoker
    {
        public static string HideElementByClassName(string classname)
        {
            return $"document.getElementsByClassName(\"{classname}\")[0].style.display = \"none\";";
        }

        public static string HideElementById(string id)
        {
            return $"document.getElementById(\"{id}\").style.display = \"none\";";
        }

        public static string GetElementData(string classname)
        {
            return $"document.getElementsByClassName(\"{classname}\")[0].innerHTML";
        }

        public static string GetElementDataByID(string id)
        {
            return $"document.getElementById(\"{id}\").innerHTML";
        }

        public static string RemoveElement(string classname)
        {
            return $"document.getElementsByClassName(\"{classname}\")[0].remove()";
        }

        public static string SetStyles(string classname,string styleProperty, string styleValue)
        {
            return $"document.getElementsByClassName(\"{classname}\")[0].style.{styleProperty} = \"{styleValue}\";";
        }

        public static string SetStylesByID(string id, string styleProperty, string styleValue)
        {
            return $"document.getElementById(\"{id}\").style.{styleProperty} = \"{styleValue}\";";
        }

        public static string GetStyles(string classname, string styleProperty)
        {
            return $"document.getElementsByClassName(\"{classname}\")[0].style.{styleProperty};";
        }

        public static string GetStylesByID(string id, string styleProperty)
        {
            return $"document.getElementById(\"{id}\").style.{styleProperty};";
        }

        public static string ExecuteClickEvent(string classname)
        {
            return $"document.getElementsByClassName(\"{classname}\")[0].click();";
        }
    }
}
