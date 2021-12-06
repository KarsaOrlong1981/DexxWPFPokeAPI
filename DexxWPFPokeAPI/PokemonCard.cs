using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DexxWPFPokeAPI
{
    public class PokemonCard
    {
        Frame frame;
        Image image;
        Grid grid;
        Button button;
        Label lbl_Name;
        Label lbl_Type;
        SolidColorBrush cardColor, textColor, typeBackColor, typeForeColor, typeBorderColor;
        Pokemon pokemon;
        bool singleView;
        public PokemonCard(Pokemon pokemon,bool singleView, Grid grid)
        {
            this.pokemon = pokemon;
            this.singleView = singleView;
            this.grid = grid;
            frame = new Frame();
            image = new Image();
            button = new Button();
            lbl_Name = new Label();
            lbl_Type = new Label();
            cardColor = new SolidColorBrush();
            textColor = new SolidColorBrush();  
            typeBackColor = new SolidColorBrush();  
            typeForeColor = new SolidColorBrush();
            typeBorderColor = new SolidColorBrush();
            

        }
      
        public Frame CreateCard()
        {
            List<string> typeList = new List<string>();
            foreach (var item in pokemon.Types)
            {
                typeList.Add(item.Type.Name);
            }
            string mainType = typeList[0];
            switch (mainType)
            {
                case "normal":
                    cardColor = Brushes.Gray;
                    typeBackColor = Brushes.LightGray;
                    typeForeColor = Brushes.Black;
                    typeBorderColor = Brushes.DarkGray;
                    textColor = Brushes.White;
                    break;
                case "fire":
                    cardColor = Brushes.Orange;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.DarkOrange;
                    typeForeColor = Brushes.Red;
                    typeBorderColor = Brushes.OrangeRed;
                    break;
                case "water":
                    cardColor = Brushes.SteelBlue;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.LightBlue;
                    typeForeColor = Brushes.DarkBlue;
                    typeBorderColor = Brushes.Blue;
                    break;
                case "grass":
                    cardColor = Brushes.SpringGreen;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.LightGreen;
                    typeForeColor = Brushes.DarkGreen;
                    typeBorderColor = Brushes.Green;
                    break;
                case "electric":
                    cardColor = Brushes.Yellow;
                    textColor = Brushes.Black;
                    typeBackColor = Brushes.DarkGoldenrod;
                    typeForeColor = Brushes.GreenYellow ;
                    typeBorderColor = Brushes.LightYellow;
                    break;
                case "ice":
                    cardColor = Brushes.DarkCyan;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.LightCyan;
                    typeForeColor = Brushes.CadetBlue;
                    typeBorderColor = Brushes.Cyan;
                    break;
                case "fighting":
                    cardColor = Brushes.Red;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.IndianRed;
                    typeForeColor = Brushes.White;
                    typeBorderColor = Brushes.OrangeRed;
                    break;
                case "poison":
                    cardColor = Brushes.Violet;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.LimeGreen;
                    typeForeColor = Brushes.Violet;
                    typeBorderColor = Brushes.PaleGreen;
                    break;
                case "ground":
                    cardColor = Brushes.SandyBrown;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.LightSalmon;
                    typeForeColor = Brushes.Brown;
                    typeBorderColor = Brushes.RosyBrown;
                    break;
                case "flying":
                    cardColor = Brushes.LavenderBlush;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.Lavender;
                    typeForeColor = Brushes.Blue;
                    typeBorderColor = Brushes.PaleVioletRed;
                    break;
                case "psychic":
                    cardColor = Brushes.Magenta;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.DarkMagenta;
                    typeForeColor = Brushes.LawnGreen;
                    typeBorderColor = Brushes.LightPink;
                    break;
                case "bug":
                    cardColor = Brushes.Olive;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.ForestGreen;
                    typeForeColor = Brushes.White;
                    typeBorderColor = Brushes.LightGreen;
                    break;
                case "rock":
                    cardColor = Brushes.Goldenrod;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.Gold;
                    typeForeColor = Brushes.DarkGray;
                    typeBorderColor = Brushes.DarkGoldenrod;
                    break;
                case "ghost":
                    cardColor = Brushes.Lavender;
                    textColor = Brushes.Black;
                    typeBackColor = Brushes.LavenderBlush;
                    typeForeColor = Brushes.DarkBlue;
                    typeBorderColor = Brushes.Blue;
                    break;
                case "dark":
                    cardColor = Brushes.Black;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.DarkGray;
                    typeForeColor = Brushes.Black;
                    typeBorderColor = Brushes.LightGray;
                    break;
                case "dragon":
                    cardColor = Brushes.Purple;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.MediumPurple;
                    typeForeColor = Brushes.PeachPuff;
                    typeBorderColor = Brushes.BlueViolet;
                    break;
                case "steel":
                    cardColor = Brushes.LightSteelBlue;
                    textColor = Brushes.White;
                    typeBackColor = Brushes.SteelBlue;
                    typeForeColor = Brushes.White;
                    typeBorderColor = Brushes.Blue;
                    break;
                case "fairy":
                    cardColor = Brushes.MistyRose;
                    textColor = Brushes.Black;
                    typeBackColor = Brushes.Pink;
                    typeForeColor = Brushes.White;
                    typeBorderColor = Brushes.Magenta;
                    break;

            }
            // dieses Frame bildet den rahmen der karte und nimmt alle daten auf
            frame = new Frame
            {
                Background = cardColor,
               
            };
            if (singleView)
            {
                frame.Width = 450;
                frame.Height = 300;
             
            }
            Grid grid = new Grid
            {
               HorizontalAlignment = HorizontalAlignment.Stretch,
               VerticalAlignment = VerticalAlignment.Stretch,
               Background = cardColor
            };

            //Zwei Spalten, Links Image , rechts die Stats
            ColumnDefinition columnDefinition1 = new ColumnDefinition();
            columnDefinition1.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinition columnDefinition2 = new ColumnDefinition();
            columnDefinition2.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(columnDefinition1);
            grid.ColumnDefinitions.Add(columnDefinition2);

            // dieses Grid nimmt die rechte seite ein und unterteilt diese in die nötige Anzahl an reihen für stats und einen Button
            Grid gridStats = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = cardColor
            };
           
            image = new Image
            {
               
                Source = new BitmapImage(new Uri(pokemon.Sprites.FrontShiny, UriKind.Absolute)),
            };
            grid.Children.Add(image);
            Grid.SetColumn(image, 0);

            // Das label für die angabe des typs
            lbl_Type = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                FontSize = 20.0,
                Foreground = typeForeColor,
                Background = typeBackColor,
                Content = mainType.ToUpper(),
                BorderBrush = typeBorderColor,
                BorderThickness = new Thickness(5)
            };
            grid.Children.Add(lbl_Type);

            // Das label für die angabe des Namen
            lbl_Name = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 25.0,
                Foreground = Brushes.Gold,
                Background = Brushes.Black,
                Content = pokemon.Name.ToUpper(),
                BorderBrush = Brushes.Gold,
                BorderThickness = new Thickness(5)
            };
            grid.Children.Add(lbl_Name);

            // Die Statistiken der pokemon in eine Liste schreiben
            List<String> statsList = new List<String>();
            foreach (var item in pokemon.Stats)
            {
                string statistics = item.Stat.Name.ToUpper() + ": " + Convert.ToString(item.BaseStat);
                statsList.Add(statistics);
            }

            //Über die eben erstellte Liste die Statistiken dem grid zuweisen
            int count = 0;
            foreach (var item in statsList)
            {
                Label label = new Label
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    FontSize = 15.0,
                    Foreground = textColor,
                    Content = item
                };
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1, GridUnitType.Star);
                gridStats.RowDefinitions.Add(rowDefinition);
                gridStats.Children.Add(label);
                Grid.SetRow(label, count);
                Grid.SetColumn(label, 1);
                count++;
            }
            grid.Children.Add(gridStats);
            Grid.SetColumn(gridStats, 1);

            // Den Button zum Schließen der karte hinzufügen
            if (singleView)
            {
                button = new Button
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    FontSize = 15.0,
                    Foreground = Brushes.Orange,
                    Background = Brushes.Black,
                    BorderBrush = Brushes.BurlyWood,
                    BorderThickness = new Thickness(5),
                    Content = "Close"

                };
                button.Click += Button_Click;
                //Eine weitere reihe für den Button hinzufügen
                RowDefinition rowDefinition1 = new RowDefinition();
                rowDefinition1.Height = new GridLength(1, GridUnitType.Star);
                gridStats.RowDefinitions.Add(rowDefinition1);
                gridStats.Children.Add(button);
                Grid.SetRow(button, count + 1);
                Grid.SetColumn(button, 1);
            }



            frame.Content = grid;
            return frame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           this.grid.Children.Remove(frame);    
        }
    }
}
