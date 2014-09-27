using System;
using System.ComponentModel;
using Android.Views;
using Beginor.X2048.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Beginor.X2048.Views.GridView), typeof(Beginor.X2048.GridViewRenderer))]

namespace Beginor.X2048 {

    public class GridViewRenderer : VisualElementRenderer<GridView> {

        protected override void OnElementChanged(ElementChangedEventArgs<GridView> e) {
            base.OnElementChanged(e);
            SetBackgroundColor(Element.BackgroundColor.ToAndroid());
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName) {
                SetBackgroundColor(Element.BackgroundColor.ToAndroid());
            }
        }

        private float startX, startY;
        private bool moved;

        public override bool DispatchTouchEvent(MotionEvent e) {
            if (e.Action == MotionEventActions.Down) {
                startX = e.GetX();
                startY = e.GetY();
                moved = false;
                return true;
            }
            if (e.Action == MotionEventActions.Move) {
                if (!moved) {
                    var deltaX = e.GetX() - startX;
                    var deltaY = e.GetY() - startY;
                    if (Math.Abs(deltaX) > 10 || Math.Abs(deltaY) > 10) {
                        if (Math.Abs(deltaX) > Math.Abs(deltaY)) {
                            Element.OnSwipe(deltaX > 0 ? SwipDirection.Right : SwipDirection.Left);
                        }
                        else {
                            Element.OnSwipe(deltaY > 0 ? SwipDirection.Down : SwipDirection.Up);
                        }
                        moved = true;
                    }
                    return true;
                }

            }
            return base.DispatchTouchEvent(e);
        }
    }

}