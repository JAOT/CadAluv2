using CadAlu.Models;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using CadAlu.Views.VistaMensagens;
using System.IO;
using CadAlu.Cells;

namespace CadAlu.Views.VistaPrincipal
{
    public class VistaPrincipal : ContentPage
    {
        public Aluno Aluno                  { get; set; }
        Turma Turma                         { get; set; }
        Escola Escola                       { get; set; }
        internal Agrupamento Agrupamento    { get; private set; }
        Button btnAvaliacoes                = new Button();
        Button btnSumarios                  = new Button();
        ListView lstAvaliacoes              = new ListView();
        ListView lstMensagens               = new ListView();
        Button btnCriarNovaMensagem         = new Button();
        ImageButton btnTiraFoto             = new ImageButton();
        Thickness margin                    = new Thickness(10);

        public VistaPrincipal(Aluno aluno)
        {
            this.Aluno = aluno;
            ObterDadosAluno();

            btnTiraFoto.Clicked += BtnTiraFoto_Clicked;

            lstMensagens.IsPullToRefreshEnabled = true;
            lstMensagens.RefreshCommand = new Command(() =>
            {
                lstMensagens.ItemsSource = ObterMensagens();
                lstMensagens.IsRefreshing = false;
            });
            lstMensagens.HasUnevenRows = true;
            lstMensagens.RowHeight = 30;

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
            InserirFotosNoBotao();
        }
        private void InserirFotosNoBotao()
        {
            var fotoBotao = Preferences.Get("Aluno" + Aluno.Id, String.Empty);
            if (!string.IsNullOrWhiteSpace(fotoBotao))
            {
                Image image = new Image
                {
                    Source = fotoBotao,
                    HeightRequest = btnTiraFoto.Height,
                    WidthRequest = btnTiraFoto.Width
                };
                btnTiraFoto.Source = fotoBotao;

            }
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
            lstMensagens.ItemTemplate = new SelectorTemplateMensagens();
            lstMensagens.ItemsSource = ObterMensagens();
            lstMensagens.ItemTapped += lstMensagens_ItemTappedAsync;

            lstAvaliacoes.ItemTemplate = new DataTemplate(typeof(ListaAvaliacoes));
            lstAvaliacoes.ItemTemplate.SetBinding(ListaMensagem.TextProperty, "Tipo");
            lstAvaliacoes.ItemTemplate.SetBinding(ListaMensagem.DetailProperty, "Aval");
            lstAvaliacoes.ItemTapped += lstAvaliacoes_ItemTappedAsync;
            lstAvaliacoes.ItemsSource = ObterAvaliacoes();
            lstAvaliacoes.IsVisible = false;
            
            StackLayout listas = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
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
            btnTiraFoto = new ImageButton
            {
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 80,
                HeightRequest = 80,
                CornerRadius = 40
            };

            StackLayout cabecalho = new StackLayout
            {
                BackgroundColor = Color.Aqua,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = margin,
                Children =
                {
                    new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        BackgroundColor = Color.Bisque,
                        Children =
                        {
                            new StackLayout
                            {
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
                            }
                        }
                    },
                    new StackLayout
                    {
                        BackgroundColor = Color.Bisque,
                        HorizontalOptions = LayoutOptions.End,
                        Children =
                        {
                            btnTiraFoto
                        }
                    }
                }
            };
            return cabecalho;
        }
        private async void BtnTiraFoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var foto = await MediaPicker.CapturePhotoAsync();
                if (foto != null)
                {
                    var f = Path.Combine(FileSystem.CacheDirectory, foto.FileName);
                    using (var stream = await foto.OpenReadAsync())
                        using (var newStream = File.OpenWrite(f))
                        await stream.CopyToAsync(newStream);
                    Preferences.Set("Aluno"+Aluno.Id, f);

                    InserirFotosNoBotao();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BtnCriarNovaMensagem_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new VistaNovaMensagem(Aluno);
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
            //marcar mensagem como lida
            var c1 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c1.Open();
            var com1 = c1.CreateCommand();
            com1.CommandText = "UPDATE mensagens  SET Lida = '1' WHERE IDENTIDADE = '" + m.Id + "'";
            com1.ExecuteNonQuery();
            c1.Close();


            Application.Current.MainPage = new VistaMensagem(m, Aluno.Id);
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
                msg.Professor = r1.IsDBNull(4) ? null : GetProfessor(r1.GetInt64(4));
                msg.DataHora = r1.GetDateTime(5);
                msg.Lida = r1.GetInt64(6);
                msg.Pai = r1.IsDBNull(7) ? null : GetPai(r1.GetInt64(7));
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
                professor.Id = r1.GetInt16(0);
                professor.Nome = r1.GetString(1);
            }
            c1.Close();
            return professor;
        }
        private Pai GetPai(long id)
        {
            var c1 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c1.Open();
            var com1 = c1.CreateCommand();
            com1.CommandText = "SELECT * FROM pais WHERE identidade = '" + id + "'";
            var r1 = com1.ExecuteReader();
            Pai pai = new Pai();
            while (r1.Read())
            {
                pai.Id = r1.GetInt16(0);
                pai.Nome = r1.GetString(1);
                
            }
            c1.Close();
            return pai;
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