using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Selenium_Prenotami
{
    public class GetDate: Configuration
    {     
        public void Initialize()
        {
            try
            {
                Setup();
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
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
