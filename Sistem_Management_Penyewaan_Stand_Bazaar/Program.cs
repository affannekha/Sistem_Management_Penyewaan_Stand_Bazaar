using System.Buffers;
using System.ComponentModel.Design;

List<Stand> dataStand = new List<Stand>()
{
    new StandOutDoor("Stand out door 1", 400000),
    new StandOutDoor("Stand out door 2", 500000),
    new StandOutDoor("Stand in door 1", 700000),
    new StandOutDoor("Stand in door 2", 800000),
    new StandOutDoor("Stand premium 1", 1800000),
    new StandOutDoor("Stand premium 2", 2000000),
};

while (true)
{
    Console.WriteLine("--- Moklet Expo Management Center ---");
    Console.WriteLine("\nDaftar Stand Tersedia");

    foreach (var dk in dataStand)
    {
        dk.tampilkanInfo();
    }

    Console.WriteLine("\n1. Sewa Stand \n2. Akhiri Sewa Stand \n3. Keluar");

    Console.Write("Pilih Menu :");
    string pilihan = Console.ReadLine();

    if (pilihan == "1")
    {
        Console.WriteLine("Input Nama Stand");
        string jenis_stand = Console.ReadLine();

        Stand standDicari = dataStand.FirstOrDefault(s => s.NamaStand.Equals(jenis_stand, StringComparison.OrdinalIgnoreCase));

        if (standDicari == null)
        {
            Console.WriteLine("Stand Tidak DItemukan");
        }
        else if (standDicari.IsAvailable)
        {
            Console.WriteLine("Masukkan Jumlah Hari :");
            int hari = int.Parse(Console.ReadLine());

            double total_sewa = standDicari.hitungTotal(hari);

            standDicari.ubahStatus();

            Console.WriteLine($"\nTotal pembayaran sewa : Rp{total_sewa}");
        }
        else
        {
            Console.Write("\nMaaf, Stand tidak tersedia");

        }
    }
    else if (pilihan == "2")
    {
        Console.WriteLine("\nInput Jenis Stand :");
        string jenis_stand = Console.ReadLine();

        var cari_JenisStand = dataStand.FirstOrDefault(ck => string.Equals(ck.NamaStand, jenis_stand, StringComparison.OrdinalIgnoreCase));

        if(cari_JenisStand == null)
        {
            Console.WriteLine("Stand Tidak Ditemukan");
        }
        else if (!cari_JenisStand.IsAvailable)
        {
            cari_JenisStand.ubahStatus();
            Console.WriteLine("\nSewa Stand Berhasil Dihentikan");
        }
        else
        {
            Console.WriteLine("\nMaaf, Stand sedang tidak disewa");
        }
       
    }
    else if (pilihan == "3")
    {
        Console.WriteLine("\nTerima Kasih Telah Menggunakan Layanan Kami");
        break;
    }
    else
    {
        Console.WriteLine("\nPilihan Tidak Valid, Silakan Coba Lagi");
    }
}

class Stand
{
    protected string _namaStand;
    protected int _hargaSewaPerHari;
    protected bool _isAvailable;

    public Stand(string nama_Stand, int harga_Sewa)
    {
        _namaStand = nama_Stand;
        _hargaSewaPerHari = harga_Sewa;
        _isAvailable = true;
    }

    public string NamaStand
    {
        get { return _namaStand; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Tidak boleh di kosongi");
            }
            else
            {
                NamaStand = value;
            }
        }
    }

    public int HargaSewaPerHari
    {
        get { return _hargaSewaPerHari; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Tidak boleh negatif");
            }
            else
            {
                _hargaSewaPerHari = value;
            }
        }
    }

    public bool IsAvailable
    {
        get { return _isAvailable; }
    }

    public void tampilkanInfo()
    {
        Console.WriteLine($"Nama Stand: {_namaStand}");
        Console.WriteLine($"Harga Sewa Per Hari: {_hargaSewaPerHari}");
        Console.WriteLine($"Ketersediaan: {(IsAvailable ? "Tersedia" : "Sedang Disewa")}");
    }

    public void ubahStatus()
    {
        _isAvailable = !_isAvailable;
    }

    public virtual double hitungTotal(int jumlahhari)
    {
        return _hargaSewaPerHari * jumlahhari;
    }
}

class StandOutDoor : Stand
{
    protected double _biayaTenda = 75000;

    public StandOutDoor(string nama_Stand, int harga_Sewa)
        : base(nama_Stand, harga_Sewa)
    {
    }

    public double biayaTenda
    {
        get { return _biayaTenda; }
    }
    public override double hitungTotal(int jumlahhari)
    {
        return base.hitungTotal(jumlahhari) + biayaTenda * jumlahhari;
    }
}
class StandInDoor : Stand
{
    protected double _biayaListrik = 100000;

    public StandInDoor(string nama_Stand, int harga_Sewa)
    : base(nama_Stand, harga_Sewa)
    {
    }

    public double BiayaListrik
    {
        get { return _biayaListrik; }
    }

    public override double hitungTotal(int jumlahhari)
    {
        return base.hitungTotal(jumlahhari) + BiayaListrik * jumlahhari;
    }
}
class StandPremium : Stand
{
    protected double _biayaKeamanan = 300000;

    public StandPremium(string nama_Stand, int harga_Sewa)
        : base(nama_Stand, harga_Sewa)
    {
    }

    public double BiayaKeamanan
    {
        get { return _biayaKeamanan; }
    }

    public override double hitungTotal(int jumlahhari)
    {
        return base.hitungTotal(jumlahhari) + BiayaKeamanan;
    }
}