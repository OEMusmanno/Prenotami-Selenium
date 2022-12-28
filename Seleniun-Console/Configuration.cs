using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Selenium_Prenotami
{
    public abstract class Configuration
    {
        //agregar el mail
        internal string username { get { return ""; } }

        //agregar el password
        internal string password { get { return ""; }}

        public IWebDriver Driver;
        public Actions action;
        public WebDriverWait waiter;

        public void Setup() 
        { 
            ChromeOptions options = new ChromeOptions(); //Se usa chrome en este ejemplo
            options.AddArguments("--incognito"); //Inicia el modo incognito, si no se quiere, quitar esta linea
            options.AddArguments("--start-maximized"); //inicia maximizado
            Driver = new ChromeDriver("C://",options);//Se usa chrome en este ejemplo
            action = new Actions(Driver);
            waiter = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); //tiempo de espera para que aparezca el boton      
        }

       
    }
}
