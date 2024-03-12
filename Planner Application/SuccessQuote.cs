namespace Planner_Application
{
    //this class helps retrieve success quotes
    //this class uses the Template Method Design Pattern
    //this is a derived concrete class that inherits from the Quote base class
    public class SuccessQuote : Quote
    {
        //this function helps retrieve success quotes
        //this function hook overrides the base class function and provides implementation
        protected override string getQuote()
        {
            //instantiates Random object
            Random rnd = new Random();

            //contains success quotes
            string[] successQuotes = {
                "All our dreams can come true; if we have the courage to pursue them.",
                "However difficult life may seem, there is always something you can do and succeed at.",
                "People begin to become successful the minute they decide to be.",
                "It always seems impossible until it’s done.",
                "Nothing is impossible, the word itself says ‘I’m possible’!",
                "Success isn’t overnight. It’s when everyday you get a little better than the day before. It all adds up.",
                "It does not matter how slowly you go as long as you do not stop.",
                "The more you praise and celebrate your life, the more there is in life to celebrate.",
                "Do what you can, with what you’ve got, where you are.",
                "Success consists of going from failure to failure without loss of enthusiasm.",
                "Women, like men, should try to do the impossible. And when they fail, their failure should be a challenge to others.",
                "Victory is sweetest when you’ve known defeat.",
                "Satisfaction lies in the effort, not in the attainment, full effort is full victory.",
                "Energy and persistence conquer all things.",
                "Our greatest weakness lies in giving up. The most certain way to succeed is always to try just one more time.",
                "The only limit to our realization of tomorrow will be our doubts of today.",
                "It is better to fail in originality than to succeed in imitation.",
                "A man can succeed at almost anything for which he has unlimited enthusiasm.",
                "In most things success depends on knowing how long it takes to succeed.",
                "There are no limits. There are only plateaus, and you must not stay there — you must go beyond them."
            };

            //generates a random between 0 and the size of the successQuotes array
            int randomNum = rnd.Next(0, successQuotes.Length);

            //return random success quote
            return successQuotes[randomNum];
        }

    }
}
