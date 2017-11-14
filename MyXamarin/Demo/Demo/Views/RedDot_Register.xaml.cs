using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RedDot_Register : CarouselPage
    {
        public RedDot_Register()
        {
            InitializeComponent();

            picSalutationBasic.Items.Add("-- Please Select --");
            picSalutationBasic.Items.Add("Toàn");
            picSalutationBasic.Items.Add("Điệp");
            picSalutationBasic.Items.Add("Ngân");
            picSalutationBasic.Items.Add("Vân");
        }

        private void picSalutationBasic_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var name = picSalutationBasic.Items[picSalutationBasic.SelectedIndex];
        }
    }
}