using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimarkets
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            { 
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukkan User ID: ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password: ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan: ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = DESKTOP-TV0IMN6\\FAHMA_ROSYI; initial catalog = {0}; User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Ubah Data");
                                        Console.WriteLine("4. Delete Data");
                                        Console.WriteLine("5. Cari Data");
                                        Console.WriteLine("6. Keluar");
                                        Console.Write("Enter your choice (1-6): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA Barang\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA BARANg\n");
                                                    Console.WriteLine("Masukkan Id Barang: ");
                                                    string id_brg = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Barang: ");
                                                    string NamaBrg = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Harga Barang: ");
                                                    string harga = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Stok: ");
                                                    string stok = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(id_brg, NamaBrg, harga, stok, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                {

                                                }
                                                break;
                                            case '4':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Masukkan Id Barang yang akan dihapus: ");
                                                    string hapus = Console.ReadLine();
                                                    pr.delete(hapus);
                                                }
                                                break;
                                            case '5':
                                                {

                                                }
                                                break;
                                            case '6':
                                                {
                                                    conn.Close();
                                                    return;
                                                }
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From dbo.Barang", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string id_brg, string NamaBrg, string harga, string stok, SqlConnection con)
        {
            string str = "";
            str = "insert into dbo.Barang (Id_Barang, Nama_Barang, Harga, Stok) values(@Id_Barang,@Nama_Barang,@Harga,@Stok)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("Id_Barang", id_brg));
            cmd.Parameters.Add(new SqlParameter("Nama_Barang", NamaBrg));
            cmd.Parameters.Add(new SqlParameter("Harga", harga));
            cmd.Parameters.Add(new SqlParameter("Stok", stok));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
        public void update()
        {
            string str = "";
            str = "update dbo.Barang where Id_Barang = ";
        }
        public void delete(string hapus)
        {
            string str = "";
            str = "delete from dbo.Barang where Id_Barang = ";


        }
    }
}

