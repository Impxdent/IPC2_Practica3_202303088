namespace IPC2_Practica3_202303088.Models
{
    public class Producto
    {
        private int id;
        private string nombre;
        private string descripcion;
        private string categoria;
        private decimal precio;
        private int cantidadStock;
        private DateTime? fechaVencimiento;

        // Getters
        public int GetId() => id;
        public string GetNombre() => nombre;
        public string GetDescripcion() => descripcion;
        public string GetCategoria() => categoria;
        public decimal GetPrecio() => precio;
        public int GetCantidadStock() => cantidadStock;
        public DateTime? GetFechaVencimiento() => fechaVencimiento;

        // Setters
        public void SetId(int val) => id = val;
        public void SetNombre(string val) => nombre = val;
        public void SetDescripcion(string val) => descripcion = val;
        public void SetCategoria(string val) => categoria = val;
        public void SetPrecio(decimal val) => precio = val;
        public void SetCantidadStock(int val) => cantidadStock = val;
        public void SetFechaVencimiento(DateTime? val) => fechaVencimiento = val;
    }
}