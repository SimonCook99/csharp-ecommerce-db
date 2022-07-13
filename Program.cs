using csharp_ecommerce_db;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


//connesione temporanea per cancellare tutti i dati delle tabelle automaticamente
using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=db-ecommerce;Integrated Security=True")){
    try{
        conn.Open();

        string queryPivot = "DELETE FROM Order_Products";
        string queryOrders = "DELETE FROM orders";
        string queryCustomers = "DELETE FROM customers";
        string queryProducts = "DELETE FROM products";

        SqlCommand cmdPivot = new SqlCommand(queryPivot, conn);
        SqlCommand cmdOrders = new SqlCommand(queryOrders, conn);
        SqlCommand cmdCustomers = new SqlCommand(queryCustomers, conn);
        SqlCommand cmdProducts = new SqlCommand(queryProducts, conn);

        cmdPivot.ExecuteNonQuery();
        cmdOrders.ExecuteNonQuery();
        cmdCustomers.ExecuteNonQuery();
        cmdProducts.ExecuteNonQuery();

    }
    catch (Exception e){
        Console.WriteLine(e.Message);
    }
    finally{
        conn.Close();
    }

}

using (ECommerceContext db = new ECommerceContext()){

    

    try{

        //MILESTONE 1
        Products prodotto1 = new Products("pomodori", "pomodori fveschi, tutto fvesco", 2.50);
        Products prodotto2 = new Products("prosciutto crudo", "prosciutto crudo di Parma", 4.25);
        Products prodotto3 = new Products("mozzarella", "mozzarella di bufala campana", 5.00);

        db.Add(prodotto1);
        db.Add(prodotto2);
        db.Add(prodotto3);

        db.SaveChanges();
        Console.WriteLine("Prodotti aggiunti!");

        //MILESTONE 2
        Customers simone = new Customers("simone", "test", "simone.test@prova.it");
        Customers Paolo = new Customers("Paolo", "prova", "paolo@prova.it");

        db.Add(simone);
        db.Add(Paolo);
        db.SaveChanges();
        Console.WriteLine("Utenti aggiunti!");

        Orders oTest = new Orders(DateTime.Now, 72.80, "in attesa di conferma", simone, new List<Products> { prodotto1, prodotto3 });

        List<Orders> list = new List<Orders>{
            new Orders(new DateTime(2021, 12, 10), 30.50, "consegnato", simone, new List<Products>{prodotto1, prodotto2}),
            new Orders(new DateTime(2022, 7, 11), 45.25, "ordine confermato", Paolo, new List<Products>{prodotto3}),
            new Orders(new DateTime(2022, 6, 21), 15.30, "in consegna", Paolo, new List<Products>{prodotto1, prodotto2, prodotto3}),
            oTest,
            new Orders(new DateTime(2022, 4, 30), 10.25, "consegnato", simone, new List<Products>{prodotto2, prodotto3})
        };


        foreach (Orders o in list){
            db.Add(o); //aggiungo l'ordine al db
            db.SaveChanges();
            order_product pivot = new order_product();
            //una volta creata l'istanza della tabella pivot, setto gli ID corrispondenti, e una quantità randomica
            pivot.ProductsId = prodotto1.ProductsId;
            pivot.OrdersId = o.Id;
            pivot.Quantity = new Random().Next(1, 10);
            db.Add(pivot);
            db.SaveChanges();
        }

        db.SaveChanges();
        Console.WriteLine("Ordini aggiunti!");

        //MILESTONE 3
        StampaOrdiniUtente(simone);
        
        //MILESTONE 4, aggiorno uno degli ordini, per poi stamparli nuovamente
        db.Database.ExecuteSqlRaw("UPDATE orders SET Status='confermato' WHERE Id=74");
        db.Entry(oTest).Reload();

        StampaOrdiniUtente(simone);

        //MILESTONE 5, cancello un ordine associato ad un cliente
        db.Database.ExecuteSqlRaw("DELETE FROM orders WHERE Id=75");

        StampaOrdiniUtente(simone);

        //MILESTONE 6, cancellazione di un prodotto con ordine associato
        db.Database.ExecuteSqlRaw("DELETE FROM products WHERE ProductsId=52");

        StampaOrdiniUtente(simone);

    }
    catch (Exception e){
        Console.WriteLine(e.Message);
    }


    void StampaOrdiniUtente(Customers customer){

        Console.WriteLine("-----Ordini dell'utente " + customer.Name + " -----");


        //lista degli ordini di un singolo utente
        List<Orders> ordiniUtente = db.Orders.FromSqlRaw("SELECT * FROM orders WHERE CustomersId=" + customer.CustomersId).ToList<Orders>();

        foreach (Orders o in ordiniUtente){
            Console.WriteLine($"Ordine in data {o.Date} di prezzo {o.Amount} con stato {o.Status}");
        }
    }

}


