using SoftTradePlus.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftTradePlus.Model
{
    
    public static class SQLHelper
    {
        public static List<Manager> GetAllManagers()
        {
            using(ApplicationContext bd = new ApplicationContext())
            {
                var result = bd.Managers.ToList();
                return result;
            }
        }
        public static List<Client> GetAllClients()
        {
            using (ApplicationContext bd = new ApplicationContext())
            {
                var result = bd.Clients.ToList();
                return result;
            }
        }
        public static List<Product> GetAllProducts()
        {
            using (ApplicationContext bd = new ApplicationContext())
            {
                var result = bd.Products.ToList();
                return result;
            }
        }
        public static List<Transaction> GetAllTransactions()
        {
            using (ApplicationContext bd = new ApplicationContext())
            {
                var result = bd.Transactions.ToList();
                return result;
            }
        }
        public static void CreateManager(string name)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                Manager newManager = new Manager { Name = name };
                db.Managers.Add(newManager);
                db.SaveChanges();
            }
        }
        public static void CreateClient(string name, int managerId, ClientStatus status) 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Client newClient = new Client { Name = name, ManagerId = managerId, Status = status };
                db.Clients.Add(newClient);
                db.SaveChanges();
            }
        }
        public static void CreateProduct(string name, float price, ProductType type)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Product newProduct = new Product { Name = name, Price = price, Type = type};
                db.Products.Add(newProduct);
                db.SaveChanges();
            }
        }
        public static void CreateTransaction(Client client, Product product, Duration duration)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Transaction newTransaction = new Transaction 
                { 
                    ClientId = client.Id, 
                    ProductId = product.Id, 
                    IsUnlimited = (product.Type == ProductType.Unlimited) 
                };
                if (product.Type == ProductType.Limited)
                {
                    newTransaction.PurchaseDate = DateTime.Today;
                    switch (duration)
                    {
                        case Duration.Month:
                            newTransaction.ExpiryDate = DateTime.Today.AddMonths(1);
                            return;
                        case Duration.Quarter:
                            newTransaction.ExpiryDate = DateTime.Today.AddMonths(3);
                            return;
                        case Duration.Year:
                            newTransaction.ExpiryDate = DateTime.Today.AddYears(1);
                            return;
                    }
                }
                db.Transactions.Add(newTransaction);
                db.SaveChanges();
            }
        }
        public static void DeleteManager(Manager manager)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Managers.Remove(manager);
                db.SaveChanges();
            }
        }
        public static void DeleteClient(Client client)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Clients.Remove(client);
                db.SaveChanges();
            }
        }
        public static void DeleteProduct(Product product)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
        public static void DeleteTransaction(Transaction transaction)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Transactions.Remove(transaction);
                db.SaveChanges();
            }
        }
        public static void EditManager(Manager oldManager, string newName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Manager manager = db.Managers.FirstOrDefault(m => m.Id == oldManager.Id);
                if(manager != null)
                {
                    manager.Name = newName;
                    db.SaveChanges();
                }
            }
        }
        public static void EditClient(Client oldClient, string newName,int newManagerId, ClientStatus newStatus)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Client client = db.Clients.FirstOrDefault(m => m.Id == oldClient.Id);
                if (client != null)
                {
                    client.Name = newName;
                    client.Status = newStatus;
                    client.ManagerId = newManagerId;
                    db.SaveChanges();
                }
            }
        }
        public static void EditProduct(Product oldProduct, string newName, float newPrice, ProductType newType)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Product product = db.Products.FirstOrDefault(m => m.Id == oldProduct.Id);
                if (product != null)
                {
                    product.Name = newName;
                    product.Price = newPrice;
                    product.Type = newType;
                    db.SaveChanges();
                }
            }
        }
        public static void EditTransaction(Transaction oldTransaction, DateTime newExpiryDate)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Transaction transaction = db.Transactions.FirstOrDefault(m => m.Id == oldTransaction.Id);
                if (transaction != null)
                {
                    transaction.ExpiryDate = newExpiryDate;
                    db.SaveChanges();
                }
            }
        }
    }
}
