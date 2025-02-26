public class Balance
{
    private int amount = 10000;
    public int Amount { get { return amount; } set { amount = value; } }

    private static Balance instance;
    private Balance() { }
    public static Balance Instance {  
        get {
            if (instance == null)
                instance = new Balance();
            return instance;
        } 
    }   
}
