namespace MauiSqliteDemo1925597
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDBService _localDBService;
        private int _editClientesId;
        public MainPage(LocalDBService dbService)
        {
            InitializeComponent();
            _localDBService = dbService;
            Task.Run(async () => listview.ItemsSource = await _localDBService.GetClientes());
        }

        

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if(_editClientesId == 0)
            {
                //Agrega cliente
                await _localDBService.Create(new Cliente
                {
                    NombreCliente = nombreEntry.Text,
                    Email = emailEntry.Text,
                    Movil = movilEntry.Text
                });
            }
            else
            {
                //Edita cliente
                await _localDBService.Update(new Cliente
                {
                    Id = _editClientesId,
                    NombreCliente = nombreEntry.Text,
                    Email = emailEntry.Text,
                    Movil = movilEntry.Text
                });
                _editClientesId = 0;
            }

            nombreEntry.Text = string.Empty;
            emailEntry.Text = string.Empty;
            movilEntry.Text = string.Empty;
            listview.ItemsSource = await _localDBService.GetClientes();
        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var clientes = (Cliente)e.Item;
            var action = await DisplayActionSheet("Acciones", "Cancelar", null, "Editar", "Eliminar");

            switch(action)
            {
                case "Editar":
                    _editClientesId = clientes.Id;
                    nombreEntry.Text = clientes.NombreCliente;
                    emailEntry.Text = clientes.Email;
                    movilEntry.Text = clientes.Movil;
                    break;

                case "Eliminar":
                    await _localDBService.Delete(clientes);
                    listview.ItemsSource = await _localDBService.GetClientes();
                    break;
            }
        }
    }

}
