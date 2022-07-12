using csharp_ecommerce_db;

using (ECommerceContext db = new ECommerceContext()){

    try{
        Products prodotto1 = new Products("pomodori", "pomodori fveschi, tutto fvesco", 2.50);
        Products prodotto2 = new Products("prosciutto crudo", "prosciutto crudo di Parma", 4.25);
        Products prodotto3 = new Products("mozzarella", "mozzarella di bufala campana", 5.00);

        db.Add(prodotto1);
        db.Add(prodotto2);
        db.Add(prodotto3);

        db.SaveChanges();
        Console.WriteLine("Prodotti aggiunti!");

        Customers simone = new Customers("simone", "test", "simone.test@prova.it");
        Customers Paolo = new Customers("Paolo", "prova", "paolo@prova.it");

        db.Add(simone);
        db.Add(Paolo);
        db.SaveChanges();
        Console.WriteLine("Utenti aggiunti!");

        List<Orders> list = new List<Orders>{
            new Orders(new DateTime(2021, 12, 10), 30.50, "consegnato", simone, new List<Products>{prodotto1, prodotto2}),
            new Orders(new DateTime(2022, 7, 11), 45.25, "ordine confermato", Paolo, new List<Products>{prodotto3}),
            new Orders(new DateTime(2022, 6, 21), 15.30, "in consegna", Paolo, new List<Products>{prodotto1, prodotto2, prodotto3}),
            new Orders(DateTime.Now, 72.80, "in attesa di conferma", simone, new List<Products>{prodotto1, prodotto3}),
            new Orders(new DateTime(2022, 4, 30), 10.25, "consegnato", simone, new List<Products>{prodotto2, prodotto3})
        };
        //Orders o1 = ;
        //Orders o2 = ;
        //Orders o3 = ;
        //Orders o4 = ;
        //Orders o5 = ;

        foreach (Orders o in list){
            db.Add(o);
        }

        db.SaveChanges();
        Console.WriteLine("Ordini aggiunti!");

    }
    catch (Exception e){
        Console.WriteLine(e.Message);
    }
    
}
