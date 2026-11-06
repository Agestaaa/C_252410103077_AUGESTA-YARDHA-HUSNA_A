using System;

namespace Sekolah
{
    class Sekolah
    {
        public string idSekolah;
        public string namaSekolah;
        public int jumlahPaketDiterima;
        public void tampilInfo()
        {
            Console.WriteLine($"sekolah meliputi {idSekolah} dan {namaSekolah} mendapat sebanyak {jumlahPaketDiterima}.");
        }
    }
    class paketMBG
    {
        private string idPaket { get; set; }
        private string namaPaket { get; set; }
        private int StokDapur
        {
            get
            {
                return StokDapur;
            }
            private set
            {
                if (value >= 0)
                {
                    StokDapur = value;
                }
                else
                {
                    Console.WriteLine("Stokdapur tidak boleh negatif!");
                }
            }
        }
        public virtual void KurangiStok(int jumlah)
        {
            if (jumlah <= 0)
            {
                Console.WriteLine(
                    "Jumlah stok tidak cukup."
                );
            }
            else if (jumlah > StokDapur)
            {
                Console.WriteLine(
                    "Stok tidak mencukupi."
                );
            }
            else
            {
                StokDapur -= jumlah;
                Console.WriteLine(
                    $"Stok mencukupi sebanyak: {jumlah}"
                );
            }
        }
    }

    class PaketReguler : paketMBG
    {
        public string Makanan;
        public int HargaPaket;
        public PaketReguler(string idpaket, string namapaket, string makanan, int hargapaket)
        {
            this.Makanan = makanan;
            this.HargaPaket = hargapaket;
        }

        public void KurangiStok(int jumlah)
        {

        }
    }

    class PaketKhusus : paketMBG
    {
        public string Makanan;
        public int HargaPaket;

        public PaketKhusus(string idpaket, string namapaket, string makanan, int hargapaket)
        {
            this.Makanan = makanan;
            this.HargaPaket = hargapaket;
        }

        public void KurangiStok(int jumlah)
        {
            Console.WriteLine();
        }
    }

    class program
    {
        static void main()
        {
            PaketReguler paketreguler1 = new PaketReguler("01", "PaketReguler", "nasi,ayam,sayur,buah", 15000);
            Console.WriteLine("--- Paket Reguler ---");
            Console.WriteLine($"ID Paket: {paketreguler1.idpaket}");
            Console.WriteLine($"Nama Paket: {paketreguler1.namapaket}");
            Console.WriteLine($"Makanan: {paketreguler1.Makanan}");
            Console.WriteLine($"HargaPaket: {paketreguler1.HargaPaket}");
        }
    };