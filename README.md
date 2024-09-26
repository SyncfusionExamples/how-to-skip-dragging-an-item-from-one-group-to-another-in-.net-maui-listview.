# how-to-skip-dragging-an-item-from-one-group-to-another-in-.net-maui-listview.

The demo explains about how to skip dragging an item from one group to another in .NET MAUI ListView(SfListView)

## XAML

 <syncfusion:SfListView x:Name="listView" AllowGroupExpandCollapse="True"
                           ItemSize="60"
                           BackgroundColor="#FFE8E8EC" 
                           GroupHeaderSize="50"
                           ItemsSource="{Binding ToDoList}"
                           DragStartMode="OnHold,OnDragIndicator"
                           SelectionMode="None">
            <syncfusion:SfListView.Behaviors>
                <local:Behavior/>
            </syncfusion:SfListView.Behaviors>

            <syncfusion:SfListView.GroupHeaderTemplate>
                <DataTemplate>
                    <Grid>
                        <Grid.BackgroundColor>
                            <OnPlatform x:TypeArguments="Color">
                                <On Platform="Android" Value="#eeeeee" />
                                <On Platform="iOS" Value="#f7f7f7" />
                                <On Platform="Windows" Value="#f2f2f2" />
                            </OnPlatform>
                        </Grid.BackgroundColor>
                        <Label Text="{Binding Key}" FontSize="14" TextColor="#333333" FontAttributes="Bold" VerticalOptions="Center" HorizontalOptions="Start" Margin="15,0,0,0" />
                    </Grid>
                </DataTemplate>
            </syncfusion:SfListView.GroupHeaderTemplate>
            <syncfusion:SfListView.ItemTemplate>
                <DataTemplate>
                  ...

                  ...
                </DataTemplate>
            </syncfusion:SfListView.ItemTemplate>
        </syncfusion:SfListView>

## C#

    public class Behavior : Behavior<SfListView>
    {
        #region Fields
        private SfListView ListView;

        #endregion

        #region Overrides
        protected override void OnAttachedTo(SfListView listview)
        {
            ListView = listview;
            ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "CategoryName",
            });
            ListView.ItemDragging += ListView_ItemDragging;
            base.OnAttachedTo(listview);
        }

        protected override void OnDetachingFrom(SfListView listview)
        {
            listview.ItemDragging -= ListView_ItemDragging;
            base.OnDetachingFrom(listview);
        }
        #endregion

        #region CallBacks
        private void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if (e.Action == DragAction.Dragging)
            {
                var currentGroup = this.GetGroup(e.DataItem);
                var container = this.ListView.GetVisualContainer();
                var groupIndex = this.ListView.DataSource.Groups.IndexOf(currentGroup);
                var nextGroup = (groupIndex + 1 < this.ListView.DataSource.Groups.Count) ? this.ListView.DataSource.Groups[groupIndex + 1] : null;
                ListViewItem groupItem = null;
                ListViewItem nextGroupItem = null;

                foreach (ListViewItem item in container.Children)
                {
                    if (item.BindingContext == null || !item.IsVisible)
                        continue;

                    if (item.BindingContext.Equals(currentGroup))
                        groupItem = item;

                    if (nextGroup != null && item.BindingContext.Equals(nextGroup))
                        nextGroupItem = item;
                }

                if (groupItem != null && e.Bounds.Y <= groupItem.Y + groupItem.Height || nextGroupItem != null && (e.Bounds.Y + e.Bounds.Height >= nextGroupItem.Y))
                    e.Handled = true;
            }
        }
        private GroupResult GetGroup(object itemData)
        {
            GroupResult itemGroup = null;

            foreach (var item in this.ListView.DataSource.DisplayItems)
            {
                if (item is GroupResult)
                    itemGroup = item as GroupResult;

                if (item == itemData)
                    break;
            }
            return itemGroup;
        }
        #endregion
    }

## Requirements to run the demo

* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) or [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/)
* Xamarin add-ons for Visual Studio (available via the Visual Studio installer).

## Troubleshooting

### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.