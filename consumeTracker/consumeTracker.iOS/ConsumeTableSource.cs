using consumeTracker.Models;
using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace consumeTracker.iOS
{
    class ConsumeTableSource : UITableViewSource
    {
        List<ConsumeItem> _consumeList;
        string CellIdentifier = "CustomCell";

        ViewController _owner;

        public ConsumeTableSource(List<ConsumeItem> items, ViewController owner)
        {
            _consumeList = items;
            _owner = owner;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier);

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);
            }

            cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
            cell.TextLabel.Lines = 0;

            // set colors
            cell.DetailTextLabel.TextColor = UIColor.Black;
            cell.TextLabel.Text = _consumeList[indexPath.Row].Store;
            cell.DetailTextLabel.Text = _consumeList[indexPath.Row].Amount.ToString() + " €"
                + " / " + string.Format("{0:0.00}", _consumeList[indexPath.Row].Date);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _consumeList.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier);

            cell = tableView.DequeueReusableCell(CellIdentifier);
            cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);

            // Configure the cell
            cell.DetailTextLabel.TextColor = UIColor.Black;
            cell.TextLabel.Text = _consumeList[indexPath.Row].Store;
            cell.DetailTextLabel.Text = _consumeList[indexPath.Row].Amount.ToString() + " €"
                + " / " + string.Format("{0:0.00}", _consumeList[indexPath.Row].Date);

            // Layout the cell
            cell.LayoutIfNeeded();

            // Get the height for the cell
            var height = cell.ContentView.SystemLayoutSizeFittingSize(UIView.UILayoutFittingCompressedSize).Height;

            // Padding for cell ceparator
            return height + 15;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            // set the current item as selected consume item
            Core.SetSelectedConsumeItem(_consumeList[indexPath.Row]);

            //_owner.movetoDetailView();
        }

    }
}
