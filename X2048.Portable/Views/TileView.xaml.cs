using Xamarin.Forms;
using Beginor.X2048.Models;
using System.Threading.Tasks;

namespace Beginor.X2048.Views {

    public partial class TileView : ContentView {

        public Tile ViewModel {
            get {
                return BindingContext as Tile;
            }
            set {
                BindingContext = value;
            }
        }

        protected override void OnBindingContextChanged() {
            base.OnBindingContextChanged();
            if (ViewModel != null) {
                UpdateBounds();
                ViewModel.PositionUpdated += OnViewModelPositionUpdated;
            }
        }

        public TileView() {
            InitializeComponent();
            //Opacity = 0.2;
            //Scale = 0.2;
        }

        protected override void OnParentSet() {
            base.OnParentSet();
            this.Animate(name: "ScaleAndFade", callback: d => {
                Opacity = d;
                Scale = d;
            }, start: 0.2, end: 1.0);
            //await this.FadeTo(1.0);
        }

        async void OnViewModelPositionUpdated(object sender, System.EventArgs e) {
            await UpdateBoundsAsync();
        }

        private void UpdateBounds() {
            var pos = (Tile)BindingContext;
            var rect = new Rectangle(pos.X * AppConsts.TileSize, pos.Y * AppConsts.TileSize, AppConsts.TileSize, AppConsts.TileSize);
            AbsoluteLayout.SetLayoutBounds(this, rect);
        }

        private async Task UpdateBoundsAsync() {
            var pos = (Tile)BindingContext;
            var rect = new Rectangle(pos.X * AppConsts.TileSize, pos.Y * AppConsts.TileSize, AppConsts.TileSize, AppConsts.TileSize);
            await this.LayoutTo(rect);
            AbsoluteLayout.SetLayoutBounds(this, rect);
        }
    }
}

