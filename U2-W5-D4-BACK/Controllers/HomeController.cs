using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using U2_W5_D4_BACK.Models;

namespace U2_W5_D4_BACK.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Scarpe.listaScarpe.Clear();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToScarpeDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@Attivo", true);
                command.CommandText = "SELECT * FROM ScarpeUnSacco WHERE ProdottoAttivo = @Attivo";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Scarpe scarpa = new Scarpe();
                        scarpa.IDProdotto = Convert.ToInt32(reader["IDProdotto"]);
                        scarpa.NomeProdotto = reader["NomeProdotto"].ToString();
                        scarpa.ImmagineCover = reader["ImmagineCover"].ToString();
                        scarpa.ImmagineDescrizione1 = reader["ImmagineDescrizione1"].ToString();
                        scarpa.ImmagineDescrizione2 = reader["ImmagineDescrizione2"].ToString();
                        scarpa.Descrizione = reader["Descrizione"].ToString();
                        scarpa.PrezzoProdotto = Convert.ToDecimal(reader["Prezzo"]);
                        scarpa.ProdottoAttivo = Convert.ToBoolean(reader["ProdottoAttivo"]);
                        Scarpe.listaScarpe.Add(scarpa);
                    }
                }

                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
            }

            return View(Scarpe.listaScarpe);
        }

        public ActionResult ListaProdotti()
        {
            List<Scarpe> listaScarpe = new List<Scarpe>();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToScarpeDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM ScarpeUnSacco";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Scarpe scarpa = new Scarpe();
                        scarpa.IDProdotto = Convert.ToInt32(reader["IDProdotto"]);
                        scarpa.NomeProdotto = reader["NomeProdotto"].ToString();
                        scarpa.ImmagineCover = reader["ImmagineCover"].ToString();
                        scarpa.ImmagineDescrizione1 = reader["ImmagineDescrizione1"].ToString();
                        scarpa.ImmagineDescrizione2 = reader["ImmagineDescrizione2"].ToString();
                        scarpa.Descrizione = reader["Descrizione"].ToString();
                        scarpa.PrezzoProdotto = Convert.ToDecimal(reader["Prezzo"]);
                        scarpa.ProdottoAttivo = Convert.ToBoolean(reader["ProdottoAttivo"]);
                        listaScarpe.Add(scarpa);
                    }
                }

                con.Close();

            }catch (Exception ex)
            {
                con.Close();
            }

            return View(listaScarpe);
        }

        public ActionResult Edit(int id)
        {
            SqlConnection con = new SqlConnection();
            Scarpe scarpa = new Scarpe();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToScarpeDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@ID", id);
                command.CommandText = "SELECT * FROM ScarpeUnSacco WHERE  IDProdotto = @ID";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();

               

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        scarpa.IDProdotto = Convert.ToInt32(reader["IDProdotto"]);
                        scarpa.NomeProdotto = reader["NomeProdotto"].ToString();
                        scarpa.ImmagineCover = reader["ImmagineCover"].ToString();
                        scarpa.ImmagineDescrizione1 = reader["ImmagineDescrizione1"].ToString();
                        scarpa.ImmagineDescrizione2 = reader["ImmagineDescrizione2"].ToString();
                        scarpa.Descrizione = reader["Descrizione"].ToString();
                        scarpa.PrezzoProdotto = Convert.ToDecimal(reader["Prezzo"]);
                        scarpa.ProdottoAttivo = Convert.ToBoolean(reader["ProdottoAttivo"]);
                    }
                }

              

            }
            catch (Exception ex)
            {

            }

            con.Close();
            
            return View(scarpa);
        }

        [HttpPost]
        public ActionResult Edit(Scarpe scarpa, HttpPostedFileBase fileUpload1, HttpPostedFileBase fileUpload2, HttpPostedFileBase fileUpload3)
        {
            SqlConnection con = new SqlConnection();

            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToScarpeDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();

                command.Parameters.AddWithValue("@ID", scarpa.IDProdotto);
                command.Parameters.AddWithValue("@Nome", scarpa.NomeProdotto);
                if(fileUpload1.ContentLength > 0)
                {
                    string fileName = fileUpload1.FileName;
                    string path = Server.MapPath("/Content/FileUpload/" + fileName);
                    fileUpload1.SaveAs(path);
                    command.Parameters.AddWithValue("@ImmagineCover", fileName);
                }
                if (fileUpload2.ContentLength > 0)
                {
                    string fileName = fileUpload2.FileName;
                    string path = Server.MapPath("/Content/FileUpload/" + fileName);
                    fileUpload1.SaveAs(path);
                    command.Parameters.AddWithValue("@ImmagineDescrizione1", fileName);
                }
                if (fileUpload3.ContentLength > 0)
                {
                    string fileName = fileUpload3.FileName;
                    string path = Server.MapPath("/Content/FileUpload/" + fileName);
                    fileUpload1.SaveAs(path);
                    command.Parameters.AddWithValue("@ImmagineDescrizione2", fileName);
                }
                command.Parameters.AddWithValue("@Descrizione", scarpa.Descrizione);
                command.Parameters.AddWithValue("@Prezzo", scarpa.PrezzoProdotto);
                command.Parameters.AddWithValue("@ProdottoAttivo", scarpa.ProdottoAttivo);
                command.CommandText = "UPDATE ScarpeUnSacco SET NomeProdotto = @Nome, Prezzo = @Prezzo, Descrizione = @Descrizione, ImmagineCover = @ImmagineCover, ImmagineDescrizione1 = @ImmagineDescrizione1, ImmagineDescrizione2 = @ImmagineDescrizione2, ProdottoAttivo = @ProdottoAttivo WHERE IDProdotto = @ID";

                command.Connection = con;
                command.ExecuteNonQuery();
            } catch (Exception ex)
            {

            }

            con.Close();
            
            return RedirectToAction("ListaProdotti");
        }


        public ActionResult Delete(int id)
        {
            SqlConnection con = new SqlConnection();
            Scarpe scarpa = new Scarpe();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToScarpeDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@ID", id);
                command.CommandText = "SELECT * FROM ScarpeUnSacco WHERE  IDProdotto = @ID";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();



                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        scarpa.IDProdotto = Convert.ToInt32(reader["IDProdotto"]);
                        scarpa.NomeProdotto = reader["NomeProdotto"].ToString();
                        scarpa.ImmagineCover = reader["ImmagineCover"].ToString();
                        scarpa.PrezzoProdotto = Convert.ToDecimal(reader["Prezzo"]);
                    }
                }



            }
            catch (Exception ex)
            {

            }

            con.Close();

            return View(scarpa);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            SqlConnection con = new SqlConnection();

            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToScarpeDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();

                
                command.Parameters.AddWithValue("@ProdottoAttivo", false);
                command.Parameters.AddWithValue("@ID", id);
                command.CommandText = "UPDATE ScarpeUnSacco SET ProdottoAttivo = @ProdottoAttivo WHERE IDProdotto = @ID";

                command.Connection = con;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }

            con.Close();

            return RedirectToAction("ListaProdotti");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Scarpe scarpa, HttpPostedFileBase fileUpload1, HttpPostedFileBase fileUpload2, HttpPostedFileBase fileUpload3)
        {
            SqlConnection con = new SqlConnection();

            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToScarpeDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();

                command.Parameters.AddWithValue("@ID", scarpa.IDProdotto);
                command.Parameters.AddWithValue("@NomeProdotto", scarpa.NomeProdotto);
                if (fileUpload1.ContentLength > 0)
                {
                    string fileName = fileUpload1.FileName;
                    string path = Server.MapPath("/Content/FileUpload/" + fileName);
                    fileUpload1.SaveAs(path);
                    command.Parameters.AddWithValue("@ImmagineCover", fileName);
                }
                if (fileUpload2.ContentLength > 0)
                {
                    string fileName = fileUpload2.FileName;
                    string path = Server.MapPath("/Content/FileUpload/" + fileName);
                    fileUpload1.SaveAs(path);
                    command.Parameters.AddWithValue("@ImmagineDescrizione1", fileName);
                }
                if (fileUpload3.ContentLength > 0)
                {
                    string fileName = fileUpload3.FileName;
                    string path = Server.MapPath("/Content/FileUpload/" + fileName);
                    fileUpload1.SaveAs(path);
                    command.Parameters.AddWithValue("@ImmagineDescrizione2", fileName);
                }
                command.Parameters.AddWithValue("@Descrizione", scarpa.Descrizione);
                command.Parameters.AddWithValue("@Prezzo", scarpa.PrezzoProdotto);
                command.Parameters.AddWithValue("@ProdottoAttivo", scarpa.ProdottoAttivo);
                command.CommandText = "INSERT INTO ScarpeUnSacco VALUES (@NomeProdotto, @Prezzo, @Descrizione, @ImmagineCover, @ImmagineDescrizione1, @ImmagineDescrizione2,  @ProdottoAttivo)";

                command.Connection = con;
                command.ExecuteNonQuery();
            }catch(Exception ex)
            {

            }

            con.Close();
            
            return RedirectToAction("ListaProdotti");
        }

        public ActionResult Details(int id)
        {
            SqlConnection con = new SqlConnection();
            Scarpe scarpa = new Scarpe();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToScarpeDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@ID", id);
                command.CommandText = "SELECT * FROM ScarpeUnSacco WHERE IDProdotto = @ID";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        scarpa.IDProdotto = Convert.ToInt32(reader["IDProdotto"]);
                        scarpa.NomeProdotto = reader["NomeProdotto"].ToString();
                        scarpa.ImmagineCover = reader["ImmagineCover"].ToString();
                        scarpa.ImmagineDescrizione1 = reader["ImmagineDescrizione1"].ToString();
                        scarpa.ImmagineDescrizione2 = reader["ImmagineDescrizione2"].ToString();
                        scarpa.Descrizione = reader["Descrizione"].ToString();
                        scarpa.PrezzoProdotto = Convert.ToDecimal(reader["Prezzo"]);
                        scarpa.ProdottoAttivo = Convert.ToBoolean(reader["ProdottoAttivo"]);
                        
                    }
                }

                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
            }

            return View(scarpa);
        }
    }
}