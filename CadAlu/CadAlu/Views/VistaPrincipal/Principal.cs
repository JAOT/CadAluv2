using CadAlu.Models;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using CadAlu.Views.VistaMensagens;
namespace CadAlu.Views.VistaPrincipal
{
    public class Principal : ContentPage
    {
        public Aluno Aluno { get; set; }
        Turma Turma { get; set; }
        Escola Escola { get; set; }
        internal Agrupamento Agrupamento { get; private set; }

        Button btnAvaliacoes = new Button();
        Button btnSumarios = new Button();


        ListView lstAvaliacoes = new ListView();
        ListView lstMensagens = new ListView();
        Button btnCriarNovaMensagem = new Button();
        Button btnMensagens = new Button();

        SensorSpeed speed = SensorSpeed.Fastest;
        Thickness margin = new Thickness(10);

        public Principal(Aluno aluno)
        {
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            ToggleAccelerometer();

            this.Aluno = aluno;
            ObterDadosAluno();

            lstMensagens.IsPullToRefreshEnabled = true;
            lstMensagens.RefreshCommand = new Command(() =>
            {
                lstMensagens.ItemsSource = ObterMensagens();
                lstMensagens.IsRefreshing = false;
            });

            StackLayout allPage = new StackLayout
            {
                Margin = margin,
                BackgroundColor = Color.LightGray,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    AdicionarCabecalho(),
                    AdicionarListas(),
                    AdicionarBotaoNovaMensagem(),
                    AdicionarBotoes()
                }

            };

            this.Content = allPage;
        }

        private View AdicionarBotaoNovaMensagem()
        {
            btnCriarNovaMensagem.Text = "+";
            btnCriarNovaMensagem.WidthRequest = 100;
            btnCriarNovaMensagem.HeightRequest = 100;
            btnCriarNovaMensagem.CornerRadius = 50;
            btnCriarNovaMensagem.Clicked += BtnCriarNovaMensagem_Clicked;

            StackLayout novaMensagem = new StackLayout
            {
                BackgroundColor = Color.Bisque,
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.End,
                Margin = margin,
                Children =
                        {
                            btnCriarNovaMensagem
                        }
            };
            return novaMensagem;
         }

        private View AdicionarBotoes()
        {
            btnAvaliacoes = new Button
            {
                Text = "Avaliações",
                WidthRequest = 150
            };
            btnSumarios = new Button
            {
                Text = "Sumários",
                WidthRequest = 150
            };
           
            btnAvaliacoes.Clicked += BtnAvaliacoes_Clicked;
            btnSumarios.Clicked += BtnSumarios_Clicked;

            StackLayout bottom = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.End,
                Children =
                {
                    new StackLayout
                    {
                        Orientation= StackOrientation.Horizontal,
                        Children =
                        {
                            btnAvaliacoes,
                            btnSumarios,
                        },
                         HorizontalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
            return bottom;
        }

        private View AdicionarListas()
        {
            lstMensagens.ItemTemplate = new DataTemplate(typeof(ListaMensagem));
            lstMensagens.ItemTemplate.SetBinding(ListaMensagem.TextProperty, "Tema");
            lstMensagens.ItemTemplate.SetBinding(ListaMensagem.DetailProperty, "DataHora");
            lstMensagens.ItemTapped += lstMensagens_ItemTappedAsync;
            lstMensagens.ItemsSource = ObterMensagens();
            lstMensagens.IsVisible = true;

            lstAvaliacoes.ItemTemplate = new DataTemplate(typeof(ListaAvaliacoes));
            lstAvaliacoes.ItemTemplate.SetBinding(ListaMensagem.TextProperty, "Tipo");
            lstAvaliacoes.ItemTemplate.SetBinding(ListaMensagem.DetailProperty, "Aval");
            lstAvaliacoes.ItemTapped += lstAvaliacoes_ItemTappedAsync;
            lstAvaliacoes.ItemsSource = ObterAvaliacoes();
            lstAvaliacoes.IsVisible = false;
            
            StackLayout listas = new StackLayout
            {
                Margin = margin,
                Children =
                {
                    lstMensagens, lstAvaliacoes
                }

            };
            return listas;
        }

        private View AdicionarCabecalho()
        {
            StackLayout cabecalho = new StackLayout
            {
                Margin = margin,
                Children =
                {
                    new Label
                    {
                        Text = Agrupamento.Nome
                    },
                    new Label
                    {
                        Text=Escola.Nome
                    },
                    new Label
                    {
                        Text=Turma.Nome
                    }
                }
            };
            return cabecalho;
        }

        private void BtnCriarNovaMensagem_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new VistaNovaMensagem(Aluno);
        }
        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            Console.WriteLine($"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}");
            // Process Acceleration X, Y, and Z
        }
        public void ToggleAccelerometer()
        {
            //try
            //{
            //    if (Accelerometer.IsMonitoring)
            //        //Accelerometer.Stop();
            //    else
            //        Accelerometer.Start(speed);
            //}
            //catch (FeatureNotSupportedException fnsEx)
            //{
            //    // Feature not supported on device
            //}
            //catch (Exception ex)
            //{
            //    // Other error has occurred.
            //}
        }
        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            lstMensagens.IsRefreshing = true;
        }
        async void lstAvaliacoes_ItemTappedAsync(object sender, ItemTappedEventArgs e)
        {
            await DisplayAlert("Info", Aluno.Nome, "OK");
        }
        private void BtnSumarios_Clicked(object sender, EventArgs e)
        {
            //if (lstAvaliacoes.IsVisible == false)
            //{
            //    lstAvaliacoes.IsVisible = true;
            //    lstMensagens.IsVisible = false;
            //    btnAvaliacoes.Text = "Mensagens";
            //}
            //else
            //{
            //    lstAvaliacoes.IsVisible = false;
            //    lstMensagens.IsVisible = true;
            //    btnAvaliacoes.Text = "Avaliações";
            //}
        }
        private void BtnAvaliacoes_Clicked(object sender, EventArgs e)
        {
            if (lstAvaliacoes.IsVisible == false)
            {
                lstAvaliacoes.IsVisible = true;
                lstMensagens.IsVisible = false;
                btnAvaliacoes.Text = "Mensagens";
            }
            else
            {
                lstAvaliacoes.IsVisible = false;
                lstMensagens.IsVisible = true;
                btnAvaliacoes.Text = "Avaliações";
            }
        }
        void lstMensagens_ItemTappedAsync(object sender, ItemTappedEventArgs e)
        {
            Mensagem m = (Mensagem)((ListView)sender).SelectedItem;
            Application.Current.MainPage = new VistaMensagem(m, Aluno.Id);

            //var msg = await DisplayAlert(m.Tema, m.Texto + "\n\nProfessor: " + m.Professor.Nome + "\n\n" + "Enviado :"+m.DataHora.ToShortDateString(), "Responder", "OK");
            //var rTema = "Re: " + m.Tema;
            //if (msg == true)
            //{
            //   var resposta = await DisplayPromptAsync(rTema, "Mensagem", "Enviar", "Cancelar");
            //    if (!string.IsNullOrEmpty(resposta))
            //    {
            //        var connection = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            //        connection.Open();
            //        var date = DateTime.Now.ToString();
            //        var command = connection.CreateCommand();
            //        command.CommandText = "INSERT INTO MENSAGENS (aluno, tema, texto, professor) VALUES ('" + Aluno.Id+"', '" + rTema + "', '" + resposta+"', '1')";
            //        try
            //        {
            //            var reader = command.ExecuteNonQuery();
                        
            //        }
            //        catch (Exception ex)
            //        {
            //            _ = DisplayAlert("Info", "Erro de ligação.", "OK");
            //        }
            //    }
            //}
        }
        private IEnumerable ObterAvaliacoes()
        {
            var c1 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c1.Open();
            var com1 = c1.CreateCommand();
            com1.CommandText = "SELECT a.identidade,a.aval, a.tipo, p.disciplina FROM avaliacoes a, professores p WHERE p.identidade = a.avaliador AND a.aluno ='" + Aluno.Id + "'";
            var r1 = com1.ExecuteReader();

            List<Avaliacao> avaliacoes = new List<Avaliacao>();

            while (r1.Read())
            {
                Avaliacao ava = new Avaliacao();
                ava.Id = r1.GetInt64(0);
                ava.Aval = r1.GetString(1);
                ava.Tipo = r1.GetString(2);
                ava.Disciplina = r1.GetString(3);
                avaliacoes.Add(ava);
            }
            c1.Close();

            return avaliacoes;
        }
        private IEnumerable ObterMensagens()
        {
            var c1 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c1.Open();
            var com1 = c1.CreateCommand();
            com1.CommandText = "SELECT * FROM mensagens WHERE aluno = '" + Aluno.Id + "'";
            var r1 = com1.ExecuteReader();

            List<Mensagem> mensagens = new List<Mensagem>();

            while (r1.Read())
            {
                Mensagem msg = new Mensagem();
                msg.Id = r1.GetInt64(0);
                msg.Tema = r1.GetString(2);
                msg.Texto = r1.GetString(3);
                msg.Professor = GetProfessor(r1.GetInt64(4));
                msg.DataHora = r1.GetDateTime(5);
                mensagens.Add(msg);
            }
            c1.Close();
            mensagens.Sort((m, mm) => mm.DataHora.CompareTo(m.DataHora));
            return mensagens;
        }
        private Professor GetProfessor(long id)
        {
            var c1 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c1.Open();
            var com1 = c1.CreateCommand();
            com1.CommandText = "SELECT * FROM professores WHERE identidade = '" + id + "'";
            var r1 = com1.ExecuteReader();
            Professor professor = new Professor();
            while (r1.Read())
            {
                professor.Nome = r1.GetString(1);
            }
            c1.Close();
            return professor;
        }
        void ObterDadosAluno()
        {
            var c1 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c1.Open();
            var com1 = c1.CreateCommand();
            com1.CommandText = "SELECT * FROM turmas WHERE identidade = '" + Aluno.IdTurma + "'";
            var r1 = com1.ExecuteReader();

            while (r1.Read())
            {
                Turma = new Turma();
                Turma.Id = r1.GetInt32(0);
                Turma.Nome = r1.GetString(1);
                Turma.Escola = r1.GetInt32(2);
            }
            c1.Close();

            var c2 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c2.Open();
            var com2 = c2.CreateCommand();
            com2.CommandText = "SELECT * FROM escolas WHERE identidade = '" + Turma.Escola + "'";
            var r2 = com2.ExecuteReader();

            while (r2.Read())
            {
                Escola = new Escola();
                Escola.Id = r2.GetInt32(0);
                Escola.Nome = r2.GetString(1);
                Escola.Agrupamento = r2.GetInt32(2);
            }
            c2.Close();

            var c3 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c3.Open();
            var com3 = c3.CreateCommand();

            com3.CommandText = "SELECT * FROM agrupamentos WHERE identidade = '" + Escola.Id + "'";
            var r3 = com3.ExecuteReader();

            while (r3.Read())
            {
                Agrupamento = new Agrupamento();
                Agrupamento.Id = r3.GetInt32(0);
                Agrupamento.Nome = r3.GetString(1);
            }
            c3.Close();
        }
    }
}