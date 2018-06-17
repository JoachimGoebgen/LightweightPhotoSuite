using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int ImgBoxRows = 3;
        public int ImgBoxColumns = 3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            LinkedList<Photo> temp = new LinkedList<Photo>();
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim-Laptop\Desktop\ToDo\R.I.P\Bilder Joachim\IMG_2305.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim-Laptop\Desktop\ToDo\R.I.P\Bilder Joachim\IMG_2305.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim-Laptop\Desktop\ToDo\R.I.P\Bilder Joachim\IMG_2305.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim-Laptop\Desktop\ToDo\R.I.P\Bilder Joachim\IMG_2305.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim-Laptop\Desktop\ToDo\R.I.P\Bilder Joachim\IMG_2305.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim-Laptop\Desktop\ToDo\R.I.P\Bilder Joachim\IMG_2305.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim-Laptop\Desktop\ToDo\R.I.P\Bilder Joachim\IMG_2305.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim-Laptop\Desktop\ToDo\R.I.P\Bilder Joachim\IMG_2305.JPG", new DateTime()), new HashSet<Tag>()));
            temp.AddLast(new Photo(new PhotoStub(@"C:\Users\Joachim-Laptop\Desktop\ToDo\R.I.P\Bilder Joachim\IMG_2305.JPG", new DateTime()), new HashSet<Tag>()));

            ImgBox.ItemsSource = temp.ToArray();
            ImgBox.cont
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
            
        }
    }
}
