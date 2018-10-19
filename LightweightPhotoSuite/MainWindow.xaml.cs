using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LightweightPhotoSuite
{

    public partial class MainWindow : Window
    {
        private int _imgBoxColumns;
        public int ImgBoxColumns
        {
            get { return _imgBoxColumns; }
            set
            {
                if (_imgBoxColumns == value) return;
                _imgBoxColumns = value;
                RaisePropertyChanged("ImgBoxColumns");
            }
        }
        public int ImgBoxRows = 4;

        public MainWindow()
        {
            InitializeComponent();
            ImgBoxColumns = 4;
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            LinkedList<Photo> temp = new LinkedList<Photo>();
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG", new DateTime()), new HashSet<Tag>()));

            ImgBox.ItemsSource = temp.ToArray();

            ImgBoxColumns = 2;
        }
        
        private void TagsMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PhotosMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TagsMenu_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void SettingsMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ImgBoxColumns = (new Random()).Next() % 9;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {

        }
        
        private void TagsMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PhotosMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TagsMenu_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
    
}
