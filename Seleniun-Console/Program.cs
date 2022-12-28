using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


//agregar el mail
string username = "";

//agregar el password
string password = "";

IWebDriver Driver;
Actions action;
WebDriverWait waiter;

try
{
    Console.WriteLine("--------------------------------------- Empieza el sistema ---------------------------------------");

    if (string.IsNullOrEmpty(username))
    {
        Console.WriteLine("Agregue el usuario");
        username = Console.ReadLine();
        Console.WriteLine("Agregue la contraseña");
        password = Console.ReadLine();
        Console.WriteLine("sigue el sistema");
    }

    ChromeOptions options = new ChromeOptions(); //Se usa chrome en este ejemplo
    options.AddArguments("--incognito"); //Inicia el modo incognito, si no se quiere, quitar esta linea
    options.AddArguments("--start-maximized"); //inicia maximizado
    Driver = new ChromeDriver("C://", options);//Se usa chrome en este ejemplo
    action = new Actions(Driver);
    waiter = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); //tiempo de espera para que aparezca el boton      
    bool ok = true;

    Driver.Navigate().GoToUrl($"https://prenotami.esteri.it/Home?ReturnUrl=%2fServices"); //Inicia en la pantalla del pretonami
    Driver.FindElement(By.Id("login-email")).SendKeys(username);    // agrega usuario
    Driver.FindElement(By.Id("login-password")).SendKeys(password); // agrega contraseña
    Driver.FindElement(By.XPath("//button[text()='Avanti']")).Click(); //le da click al boton
    waiter.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//button[text()='Prenota']")))[2].Click(); //busca el boton de prenota para reconstruccion
    while (ok) //bucle para evitar tener que cargar el sitio de nuevo
    {
        if (waiter.Until(ExpectedConditions.ElementExists(By.XPath("//div[text()='Al momento non ci sono date disponibili per il servizio richiesto']"))) != null) //cartel de error
        {
            Driver.FindElement(By.XPath("//button[@class='btn btn-blue']")).Click(); //boton de OK del error
            waiter.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//button[text()='Prenota']")))[2].Click(); //boton de prenota
        }
        else
        {
            ok = false;
        }
    }
}
catch (Exception e )
{
    Console.WriteLine(e.Message);
    Console.WriteLine("--------------------------------------- Se puede cerrar la ventana ---------------------------------------");
}
