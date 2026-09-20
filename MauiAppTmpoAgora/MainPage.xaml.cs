using MauiAppTmpoAgora.Models;
using MauiAppTmpoAgora.Services;

namespace MauiAppTmpoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlertAsync("Sem internet","Sem acesso à internet. Reconecte-se para utilizar o aplicativo!","OK");

                    return;
                }
                if (!string.IsNullOrEmpty(txt_cidade.Text)) 
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"{txt_cidade.Text}\n" +
                                         $"Latitude: {t.lat}\n" +
                                         $"Longitude: {t.lon}\n" +
                                         $"Descrição: {t.description}\n" +
                                         $"Nascer do Sol: {t.sunrise}\n" +
                                         $"Pôr do Sol: {t.sunset}\n" +
                                         $"Temperatura máxima: {t.temp_max}\n" +
                                         $"Temperatura mínima: {t.temp_min}\n" +
                                         $"Visibilidade: {t.visibility} m\n" +
                                         $"Velocidade: {t.speed} km/h\n";

                        lbl_res.Text = dados_previsao;

                    } else
                    {
                        lbl_res.Text = "Sem dados de previsão.";
                    }

                } else 
                {
                    lbl_res.Text = "Preencha a cidade!";
                }
            }
            catch (Exception ex) 
            {
                await DisplayAlertAsync("Ops", ex.Message, "Ok");
            }
        }
    }
}
