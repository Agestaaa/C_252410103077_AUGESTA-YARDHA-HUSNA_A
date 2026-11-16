using System;
using System.Collections.Generic;

namespace MBGApp
{
    class Sekolah
    {
        public string idSekolah;
        public string namaSekolah;
        public int jumlahPaketDiterima;

        public Sekolah(string id, string nama)
        {
            idSekolah = id;
            namaSekolah = nama;
            jumlahPaketDiterima = 0;
        }

        public void TampilInfo()
        {
            Console.WriteLine($"ID: {idSekolah} | Nama: {namaSekolah} | Paket diterima: {jumlahPaketDiterima}");
        }
    }

    abstract class PaketMBG
    {
        private string idPaket;
        private string namaPaket;
        private int stokDapur;

        public string IdPaket
        {
            get { return idPaket; }
            set { idPaket = value; }
        }

        public string NamaPaket
        {
            get { return namaPaket; }
            set { namaPaket = value; }
        }

        public int StokDapur
        {
            get { return stokDapur; }
            private set
            {
                if (value >= 0) stokDapur = value;
                else Console.WriteLine("Stok dapur tidak boleh negatif!");
            }
        }

        protected PaketMBG(string id, string nama, int stok)
        {
            idPaket = id;
            namaPaket = nama;
            StokDapur = stok;
        }

        public bool KurangiStok(int jumlah)
        {
            if (jumlah <= 0)
            {
                Console.WriteLine("Jumlah pengurangan harus lebih dari 0.");
                return false;
            }
            if (jumlah > stokDapur)
            {
                Console.WriteLine("Stok tidak mencukupi.");
                return false;
            }
            StokDapur = stokDapur - jumlah;
            return true;
        }

        public abstract int HitungBiaya(int jumlah);
    }

    class PaketReguler : PaketMBG
    {
        public string Isi = "Nasi, ayam, sayur, dan buah";

        public PaketReguler(string id, string nama, int stok)
            : base(id, nama, stok) { }

        public override int HitungBiaya(int jumlah) => jumlah * 15000;
    }

    class PaketKhusus : PaketMBG
    {
        public string JenisDiet { get; set; }

        public PaketKhusus(string id, string nama, int stok, string jenisDiet)
            : base(id, nama, stok)
        {
            JenisDiet = jenisDiet;
        }

        public override int HitungBiaya(int jumlah) => jumlah * 20000;
    }

    interface DapatDidistribusikan
    {
        void KirimPaket();
    }

    class DistribusiMBG : DapatDidistribusikan
    {
        private Sekolah sekolahTujuan;
        private PaketMBG paket;
        private int jumlah;

        public DistribusiMBG(Sekolah sekolah, PaketMBG paket, int jumlah)
        {
            this.sekolahTujuan = sekolah;
            this.paket = paket;
            this.jumlah = jumlah;
        }

        public void KirimPaket()
        {
            if (jumlah <= 0)
            {
                Console.WriteLine("Distribusi gagal: jumlah paket harus lebih dari 0.");
                return;
            }
            if (jumlah > paket.StokDapur)
            {
                Console.WriteLine($"Distribusi gagal: jumlah ({jumlah}) melebihi stok dapur ({paket.StokDapur}).");
                return;
            }

            paket.KurangiStok(jumlah);
            sekolahTujuan.jumlahPaketDiterima += jumlah;
            Console.WriteLine($"Distribusi MBG berhasil! Sebanyak {jumlah} paket makanan telah dikirim ke sekolah {sekolahTujuan.namaSekolah}");
        }
    }

    class PengelolaMBG
    {
        private List<Sekolah> daftarSekolah = new List<Sekolah>();
        private List<PaketMBG> daftarPaket = new List<PaketMBG>();

        public void TambahSekolah(Sekolah sekolah) => daftarSekolah.Add(sekolah);
        public void TambahPaket(PaketMBG paket) => daftarPaket.Add(paket);

        public void TampilkanDataMBG()
        {
            Console.WriteLine("=== Data Sekolah ===");
            foreach (var s in daftarSekolah) s.TampilInfo();

            Console.WriteLine("=== Data Paket ===");
            foreach (var p in daftarPaket)
                Console.WriteLine($"ID: {p.IdPaket} | {p.NamaPaket} | Stok: {p.StokDapur}");
        }
    }

    class Program
    {
        static void Main()
        {
            Sekolah s1 = new Sekolah("S01", "SDN 1 Jember");
            Sekolah s2 = new Sekolah("S02", "SMPN 2 Jember");
            s1.TampilInfo();
            s2.TampilInfo();

            PaketMBG p1 = new PaketReguler("P01", "Paket Reguler", 100);
            PaketMBG p2 = new PaketKhusus("P02", "Paket Khusus", 50, "Vegetarian");
            Console.WriteLine($"Biaya reguler 10 paket: Rp{p1.HitungBiaya(10)}");
            Console.WriteLine($"Biaya khusus 10 paket : Rp{p2.HitungBiaya(10)}");

            new DistribusiMBG(s1, p1, 30).KirimPaket();   
            new DistribusiMBG(s2, p2, 100).KirimPaket();  
            new DistribusiMBG(s2, p2, 0).KirimPaket();    

            PengelolaMBG pengelola = new PengelolaMBG();
            pengelola.TambahSekolah(s1);
            pengelola.TambahSekolah(s2);
            pengelola.TambahPaket(p1);
            pengelola.TambahPaket(p2);
            pengelola.TampilkanDataMBG();
        }
    }
}