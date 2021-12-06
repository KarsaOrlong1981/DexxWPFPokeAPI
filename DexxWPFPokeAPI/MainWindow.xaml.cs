using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokeApiNet;

namespace DexxWPFPokeAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Pokemon> allpokes;
        List<Pokemon> types;
        List<string> pokemonNames;
        PokeApiClient pokeClient;
        PokemonCard pokemonCard;
        ProgressBar progressBar;
        Grid showCards;
        ListView listView;
        int numberOfPoke;
        public MainWindow()
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"C:\Users\j1981\source\repos\DexxWPFPokeAPI\DexxWPFPokeAPI\pics\273289.jpg", UriKind.Absolute));
            this.Background = myBrush;
            pokeClient = new PokeApiClient();
            allpokes = new List<Pokemon>();
            types = new List<Pokemon>();
            progressBar = new ProgressBar();
            showCards = new Grid();
            pokemonNames = new List<string>();
            listView = new ListView();
            if (File.Exists("pokemonNames.txt"))
            {
                using (StreamReader sr = new StreamReader("pokemonNames.txt"))
                {
                    while (!(sr.EndOfStream))
                    {
                        pokemonNames.Add(sr.ReadLine());
                    }
                }
            }
            else

               PokemonNames();

        }

       
        //Gewünschte anzahl der Pokemon herunterladen
        async void GetallPokemon(int number)
        {

            int id = 1;
            numberOfPoke = number;
            while (id <= number)
            {
                try
                {
                    Pokemon poke = await pokeClient.GetResourceAsync<Pokemon>(id);
                    allpokes.Add(poke);
                    
                    id++;
                }
                catch
                {
                    MessageBox.Show("Lost connection to Site");
                }
            }
            CreatePokemonCards(allpokes);

        }

        async void PokemonNames()
        {
            int id = 1;
            bool findPokemon = true;
            while (findPokemon)
            {
                try
                {
                    Pokemon poke = await pokeClient.GetResourceAsync<Pokemon>(id);
                    pokemonNames.Add(poke.Name);

                    id++;
                }
                catch
                {
                   
                    findPokemon = false;
                  
                }
            }
            using (StreamWriter sw = new StreamWriter("pokemonNames.txt", false, Encoding.ASCII))
            {
                foreach (var item in pokemonNames)
                {
                    sw.WriteLine(item);
                }
            };
        }

        private void ErrorMessage()
        {
            txt_Box.Text = "";
            txt_Box.Focus();
            MessageBox.Show("Bitte nur Zahlen von 1 - 898 eingeben", "Falsche Eingabe!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Txt_Box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bool inputOK = true;
                int input = 0;
                try
                {
                    input = Convert.ToInt32(txt_Box.Text);
                    if (input < 1 || input > 898)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    ErrorMessage();
                    inputOK = false;
                }
                if (inputOK)
                {
                    this.frame.Visibility = Visibility.Hidden;
                    GetallPokemon(input);
                    progressBar = new ProgressBar
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 400,
                        Height = 50
                    };
                   
                    progressBar.Maximum = numberOfPoke - 25;
                    progressBar.IsIndeterminate = false;
                    double animation = Convert.ToDouble(numberOfPoke);
                   
                 

                    Duration duration = new Duration(TimeSpan.FromSeconds(10 * (numberOfPoke / 100)));

                    DoubleAnimation doubleAnimation = new DoubleAnimation(0.0, animation, duration);
                    progressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
                    this.grid.Children.Add(progressBar);
                    showCards.Visibility = Visibility.Collapsed;
                }
              
            }

        }
        private void CreatePokemonCards(List<Pokemon> pokemonList)
        {

            this.frame.Visibility = Visibility.Hidden;
            int moduloCount = 1;
            int counter = 0;
            int row = 0;

            Grid grid = new Grid { };
            //Die erste Reihe muss gesetzt sein
            RowDefinition rowDefinition1 = new RowDefinition();
            rowDefinition1.Height = new GridLength(400);
            grid.RowDefinitions.Add(rowDefinition1);
            //zwei Spalten fest setzen
            ColumnDefinition columnDefinition1 = new ColumnDefinition();
            columnDefinition1.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinition columnDefinition2 = new ColumnDefinition();
            columnDefinition2.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(columnDefinition1);
            grid.ColumnDefinitions.Add(columnDefinition2);
            foreach (var item in pokemonList)
            {

                int column = 0;
                pokemonCard = new PokemonCard(item, false, this.grid);
                Frame frame = pokemonCard.CreateCard();
                frame.HorizontalAlignment = HorizontalAlignment.Stretch;
                frame.VerticalAlignment = VerticalAlignment.Stretch;
                frame.Padding = new Thickness(10);
                frame.BorderBrush = new SolidColorBrush(Colors.White);
                frame.BorderThickness = new Thickness(5);
                grid.Children.Add(frame);
                Grid.SetRow(frame, row);
                if (!(counter == 0))
                {
                    if (moduloCount % 2 == 0)
                    {
                        column = 1;
                        RowDefinition rowDefinition = new RowDefinition();
                        rowDefinition.Height = new GridLength(400);
                        grid.RowDefinitions.Add(rowDefinition);
                        row++;
                    }
                }

                Grid.SetColumn(frame, column);

                //progressBar.Value = moduloCount;

                moduloCount++;
                counter++;


            }
            showCards = grid;
            this.grid.Children.Add(grid);
            Grid.SetRow(grid, 1);
            this.grid.Children.Remove(progressBar);
        }

      

        private void backMenue_Click(object sender, RoutedEventArgs e)
        {
            showCards.Visibility = Visibility.Hidden;
            this.frame.Visibility = Visibility.Visible;
        }

       

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            showCards.Visibility = Visibility.Visible;
        }

        private void buttonLos_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Visibility = Visibility.Hidden;
            List<string> searchResult = new List<string>();
            string input = txt_search .Text;
            input = input.ToLower ();
            foreach (var item in pokemonNames)
            {
                bool containsResult = item.Contains(input);
                if (containsResult)
                {
                   searchResult.Add(item); 
                }
            }
            ListView listView = new ListView()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 200,
                Margin = new Thickness(0,40,0,0),
            };
            listView.SelectionChanged += ListView_SelectionChanged;
            foreach (var item in searchResult)
            {
                listView.Items.Add(item);
            }
            this.listView = listView;
            this.grid.Children.Add(listView);  
            Grid.SetRow(listView, 0);
        }

        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.grid.Children.Remove(listView);
            this.frame.Visibility = Visibility.Hidden;
#pragma warning disable CS8600 // Das NULL-Literal oder ein möglicher NULL-Wert wird in einen Non-Nullable-Typ konvertiert.
            string name = Convert.ToString(e.AddedItems[0]);
#pragma warning restore CS8600 // Das NULL-Literal oder ein möglicher NULL-Wert wird in einen Non-Nullable-Typ konvertiert.

            Pokemon pokemon = await pokeClient.GetResourceAsync<Pokemon>(name);
            pokemonCard = new PokemonCard(pokemon, true, this.grid);
            Frame singlePokeCard = pokemonCard.CreateCard();
            singlePokeCard.HorizontalAlignment = HorizontalAlignment.Center;
            singlePokeCard.VerticalAlignment = VerticalAlignment.Center;
            singlePokeCard.Margin = new Thickness(70);
            this.grid.Children.Add(singlePokeCard);
            Grid.SetRow(singlePokeCard, 0);
        }

        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.frame.Visibility = Visibility.Hidden;
            this.grid.Children.Remove(listView);
            buttonLos_Click(sender, e);
        }
    }
}

