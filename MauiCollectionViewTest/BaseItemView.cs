using System.Diagnostics;
using Microsoft.Maui.Layouts;
using ILayout = Microsoft.Maui.ILayout;

namespace MauiCollectionViewTest;

public class BaseItemView : Layout
{
    protected override ILayoutManager CreateLayoutManager() => new ItemViewLayoutManager(this);

    private class ItemViewLayoutManager : LayoutManager
    {
        public ItemViewLayoutManager(ILayout layout) : base(layout)
        {
        }

        public override Size Measure(double widthConstraint, double heightConstraint)
        {
            Debug.WriteLine($"Measure {widthConstraint} x {heightConstraint}");
            
            if (double.IsPositiveInfinity(widthConstraint))
            {
                widthConstraint = 100;
            }

            heightConstraint = (widthConstraint / 2) * 3;

            foreach (var child in Layout)
            {
                child.Measure(widthConstraint, heightConstraint);
            }

            return new Size(widthConstraint, heightConstraint);
        }

        public override Size ArrangeChildren(Rect bounds)
        {
            Debug.WriteLine($"ArrangeChildren {bounds}");
            
            foreach (var child in Layout)
            {
                child.Arrange(bounds);
            }
            
            return bounds.Size;
        }
    }
}