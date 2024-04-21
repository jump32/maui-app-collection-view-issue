namespace MauiCollectionViewTest;

public class ItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate? ItemTemplate { get; set; }
    public DataTemplate? LoadingTemplate { get; set; }
    
    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        if (item is not Item anItem)
        {
            return null;
        }

        return anItem.IsLoading ? LoadingTemplate : ItemTemplate;
    }
}