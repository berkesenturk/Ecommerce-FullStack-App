using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EcommerceApi.Entities;
using System.Collections.Generic;
namespace EcommerceApi.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var vendorDataDict = new Dictionary<string,string>()
                {
                    {"VendorNames","Apple,Huawei,Xiaomi,Samsung,TrendyolMilla,New Balance,Lacoste,Altinyildiz,Adidas"},
                    {"FollowerCount","1000,1000,1000,1000,1000,1000,1000,1000,1000"},
                    {"Rating","8.2,7.5,4.5,7.4,4.2,6.3,8.2,8.9,9.4"}
                };

                var categoryDataDict = new Dictionary<string,string>()
                {
                    {"Name","Electronics,Textile,Sport"}
                };


                List<string> categoryList = new List<string>(categoryDataDict["Name"].Split(","));                
                List<string> vendorsList = new List<string>(vendorDataDict["VendorNames"].Split(","));
                
                // if(context.Products.Any())
                // {
                //     return;
                // }
                List<Vendor> vendorDataList = new List<Vendor>();
                for (int i = 0; i < vendorsList.Count(); i++)
                {
                    vendorDataList.Add(new Vendor
                    {
                        VendorName = vendorDataDict["VendorNames"].Split(",")[i],
                        FollowerCount = Convert.ToInt16(vendorDataDict["FollowerCount"].Split(",")[i]),
                        Rating = Convert.ToDouble(vendorDataDict["Rating"].Split(",")[i])
                    });
                }
                List<Category> categoryDataList = new List<Category>();
                for (int i = 0; i < categoryList.Count(); i++)
                {
                    categoryDataList.Add(new Category
                    {
                        Name = categoryDataDict["Name"].Split(",")[i]
                    });
                }
                //try
                //{   
                    // add vendors first
                    // context.SaveChanges();
                //}
                
                //finally
                //{   

                    List<Product> products = new List<Product>();
                
                    products.Add(
                        new Product
                        {
                            Title = "Adih80 Hybrid80 Antrenman Boks Eldiveni Boxing Gloves",
                            Price = Convert.ToDecimal("475"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty252/product/media/images/20211122/12/398847/320395737/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Sport") + 1,
                            VendorId =  vendorsList.IndexOf("Adidas") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "Uniforia League Futbol Topu",
                            Price = Convert.ToDecimal("297.99"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty3/product/media/images/20200608/16/2414532/60756088/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Sport") + 1,
                            VendorId = vendorsList.IndexOf("Adidas") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "Unisex Bone - 3 Stripes Silicone Swim Cap Yüzücü Bonesi - 802310",
                            Price = Convert.ToDecimal("58.90"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty95/product/media/images/20210403/20/beb575cc/15447386/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Sport") + 1,
                            VendorId = vendorsList.IndexOf("Adidas") + 1
                        });

                        // TEXTILE
                        products.Add(new Product
                        {
                            Title = "Siyah Yüksek Bel Nervür Dikişli Pantolon TWOSS21PL0093",
                            Price = Convert.ToDecimal("109.99"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty160/product/media/images/20210819/16/120151286/123408601/5/5_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Textile") + 1,
                            VendorId = vendorsList.IndexOf("TrendyolMilla") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "Siyah Kapüşonlu Şişme Mont TWOAW21MO0022",
                            Price = Convert.ToDecimal("269.99"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty184/product/media/images/20210929/14/137516657/79038587/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Textile") + 1,
                            VendorId = vendorsList.IndexOf("TrendyolMilla") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "Antrasit Düğme Kapamali Yünlü Kaşe Kaban TWOAW21KB0038",
                            Price = Convert.ToDecimal("299.99"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty204/product/media/images/20211020/15/152883110/76796708/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Textile") + 1,
                            VendorId = vendorsList.IndexOf("TrendyolMilla") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "Kadin Eşofman Alti - V-WTP007-BKW",
                            Price = Convert.ToDecimal("164.99"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty204/product/media/images/20211020/15/152883110/76796708/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Textile") + 1,
                            VendorId = vendorsList.IndexOf("New Balance") + 1
                        });
                                        products.Add(new Product
                        {
                            Title = "Kadin Sweatshirt - V-WTH804-BK",
                            Price = Convert.ToDecimal("164.99"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty87/product/media/images/20210402/16/f9448f73/12547756/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Textile") + 1,
                            VendorId = vendorsList.IndexOf("New Balance") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "Kadin Kapitone Blok Desenli Dik Yaka Lacivert Mont",
                            Price = Convert.ToDecimal("2499"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty176/product/media/images/20210913/10/129369956/241079740/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Electronics") + 1,
                            VendorId = vendorsList.IndexOf("Lacoste") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "Kadin Kapüşonlu Kapitone Siyah Mont",
                            Price = Convert.ToDecimal("5169"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty264/product/media/images/20211209/12/7705909/333739265/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Electronics") + 1,
                            VendorId = vendorsList.IndexOf("Lacoste") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "Kadin Slim Fit Uzun Kollu Polo Yaka Çizgili Siyah Elbise",
                            Price = Convert.ToDecimal("1979"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty263/product/media/images/20211202/17/3042049/327765927/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Electronics") + 1,
                            VendorId = vendorsList.IndexOf("Lacoste") + 1
                        });


                        // ELECTRONICS
                        products.Add(new Product
                        {
                            Title = "Redmi Note 10S 6GB + 128GB Siyah Cep Telefonu (Xiaomi Türkiye Garantili)",
                            Price = Convert.ToDecimal("3998.90"),
                            PictureUrl = "https://cdn.dsmcdn.com/ty200/product/media/images/20211014/17/147839284/113204070/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Electronics") + 1,
                            VendorId = vendorsList.IndexOf("Apple") + 1
                        });

                        products.Add(new Product
                        {
                            Title = "Galaxy M12 64GB Mavi Cep Telefonu (Samsung Türkiye Garantili)",
                            Price = Convert.ToDecimal("2771"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty223/product/media/images/20211103/12/164511374/181229873/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Electronics") + 1,
                            VendorId = vendorsList.IndexOf("Samsung") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "Galaxy A52 128GB Mavi Cep Telefonu (Samsung Türkiye Garantili)",
                            Price = Convert.ToDecimal("4699"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty132/product/media/images/20210619/10/102332652/170369806/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Electronics") + 1,
                            VendorId = vendorsList.IndexOf("Samsung") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "Galaxy A52 128GB Siyah Cep Telefonu (Samsung Türkiye Garantili)",
                            Price = Convert.ToDecimal("4699"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty132/product/media/images/20210619/10/102332652/170369806/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Electronics") + 1,
                            VendorId = vendorsList.IndexOf("Samsung") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "iPhone SE 64GB Siyah Cep Telefonu (Apple Türkiye Garantili) Aksesuarsiz Kutu",
                            Price = Convert.ToDecimal("6467.02"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty200/product/media/images/20211014/17/147839284/113204070/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Electronics") + 1,
                            VendorId = vendorsList.IndexOf("Samsung") + 1
                        });
                        products.Add(new Product
                        {
                            Title = "Redmi Note 10S 6GB + 128GB Mavi Cep Telefonu (Xiaomi Türkiye Garantili)",
                            Price = Convert.ToDecimal("3999"),
                            PictureUrl = "https://cdn.dsmcdn.com/mnresize/1200/1800/ty167/product/media/images/20210827/21/123313714/234118926/1/1_org_zoom.jpg",
                            CategoryId = categoryList.IndexOf("Electronics") + 1,
                            VendorId = vendorsList.IndexOf("Samsung") + 1
                        });

                //}
                var clientDataDict = new Dictionary<string, string>()
                {
                    // {"username","aliyildirim,selimcolak"},
                    // {"password","123,456"},
                    {"first_name","Ali,Selim"},
                    {"last_name","Yildirim,Colak"},
                    {"phone","5234353322,5234353354"},
                    {"address_line","addres1,addres2"},
                    {"city","istanbul,adana"},
                    {"postal_code","34060,20222"}
                    // create date will come with system datetime                    
                };
                
                //context.ClientPayments.AddRange(clientPaymentList);
                // CLIENT

                List<string> clientList = new List<string>(clientDataDict["phone"].Split(","));
                List<Client> clientDataList = new List<Client>();
                for (int i = 0; i < clientList.Count(); i++)
                {
                    clientDataList.Add(new Client
                    {
                        FirstName = clientDataDict["first_name"].Split(",")[i],
                        LastName = clientDataDict["last_name"].Split(",")[i],
                        Phone = clientDataDict["phone"].Split(",")[i],
                        AddressLine = clientDataDict["address_line"].Split(",")[i],
                        City = clientDataDict["city"].Split(",")[i],
                        PostalCode = clientDataDict["postal_code"].Split(",")[i],
                        CreateDate = DateTime.Today
                    });
                }
                 
                // Order Detail Data Seed
                List<OrderDetail> orderDetailDataList = new List<OrderDetail>();
                orderDetailDataList.Add(
                    new OrderDetail
                    {
                        TotalPrice = Convert.ToDecimal("500.12"),
                        ClientId = 1,
                        CreateDate = DateTime.Today
                    }
                );
                orderDetailDataList.Add(
                    new OrderDetail
                    {
                        TotalPrice = Convert.ToDecimal("100.54"),
                        ClientId = 2,
                        CreateDate = DateTime.Today
                    }
                );
                orderDetailDataList.Add(
                    new OrderDetail
                    {
                        TotalPrice = Convert.ToDecimal("1500.99"),
                        ClientId = 2,
                        CreateDate = DateTime.Today
                    }
                );

                // Payment Detail Data Seed
                List<PaymentDetail> paymentDetailDataList = new List<PaymentDetail>();
                paymentDetailDataList.Add(
                    new PaymentDetail
                        {
                            // amount will be filled from total amount
                            Amount = Convert.ToDecimal("1000"),
                            Type = "EFT",
                            Provider = "Yapi Kredi",
                            OrderDetailId = 1,
                            CreateDate = DateTime.Today
                    }
                );
                paymentDetailDataList.Add(
                    new PaymentDetail
                        {
                            Amount = Convert.ToDecimal("1000"),
                            Type = "Credit Card",
                            Provider = "Master Card",
                            OrderDetailId = 2,
                            CreateDate = DateTime.Today
                        }
                );
                paymentDetailDataList.Add(
                    new PaymentDetail
                        {
                            Amount = Convert.ToDecimal("1000"),
                            Type = "Credit Card",
                            Provider = "Visa",
                            OrderDetailId = 2,
                            CreateDate = DateTime.Today
                        }
                );
                
                // Order Item Data Seed
                List<OrderItem> orderItemDataList = new List<OrderItem>();
                orderItemDataList.Add(
                    new OrderItem
                        {
                            // amount will be filled from total amount
                            ProductId = 1,
                            OrderDetailId = 1,
                            Quantity = 2,
                            CreateDate = DateTime.Today
                        }
                );
                orderItemDataList.Add(
                    new OrderItem
                        {
                            // amount will be filled from total amount
                            ProductId = 2,
                            OrderDetailId = 1,
                            Quantity = 1,
                            CreateDate = DateTime.Today
                        }
                );
                orderItemDataList.Add(
                    new OrderItem
                        {
                            // amount will be filled from total amount
                            ProductId = 3,
                            OrderDetailId = 2,
                            Quantity = 2,
                            CreateDate = DateTime.Today
                        }
                );
                orderItemDataList.Add(
                    new OrderItem
                        {
                            // amount will be filled from total amount
                            ProductId = 4,
                            OrderDetailId = 2,
                            Quantity = 3,
                            CreateDate = DateTime.Today
                    }
                    
                );

            
                    // context.Vendors.AddRange(vendorDataList);
                    // context.Categories.AddRange(categoryDataList);
                    // context.Products.AddRange(products);
                    // context.Clients.AddRange(clientDataList);
                    // context.OrderDetails.AddRange(orderDetailDataList);
                    // context.PaymentDetails.AddRange(paymentDetailDataList);
                    // context.OrderItems.AddRange(orderItemDataList);
                    

                //context.SaveChanges();
            }
        }
    }
}
