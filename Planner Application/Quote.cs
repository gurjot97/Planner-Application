namespace Planner_Application
{
    //this class helps retrieve quotes
    //this class uses the Template Method Design Pattern
    //this is an abstract base class
    public abstract class Quote
    {
        //this function contains the hook method that helps retrieve quote data
        public string TemplateMethod()
        {
            return this.getQuote();
        }

        //this function helps retrieve quotes
        //this function needs to be implemented in the derived classes
        protected abstract string getQuote();
    }
}
