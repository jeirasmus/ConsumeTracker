using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using consumeTracker.Models;

namespace consumeTracker.iOS
{
    class StoreTableSource : UITableViewSource
    {
        List<StoreItem> _storeList;
        string CellIdentifier = "CustomCell";
        UITableViewCell selectedCell;

        AddViewController _owner;

        public StoreTableSource(List<StoreItem> items, AddViewController owner)
        {
            _storeList = items;
            _owner = owner;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier);

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }

            cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
            cell.TextLabel.Lines = 0;

            // set colors
            cell.TextLabel.Text = _storeList[indexPath.Row].Store;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _storeList.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier);

            cell = tableView.DequeueReusableCell(CellIdentifier);
            cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);

            // Configure the cell
            cell.TextLabel.Text = _storeList[indexPath.Row].Store;

            // Layout the cell
            cell.LayoutIfNeeded();

            // Get the height for the cell
            var height = cell.ContentView.SystemLayoutSizeFittingSize(UIView.UILayoutFittingCompressedSize).Height;

            // Padding for cell ceparator
            return height + 15;
        }
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var currentSelectedCell = tableView.CellAt(indexPath);
            if (currentSelectedCell != selectedCell)
            {
                if (selectedCell != null)
                {
                    selectedCell.Accessory = UITableViewCellAccessory.None;
                }
                selectedCell = currentSelectedCell;
            }

            currentSelectedCell.Accessory = UITableViewCellAccessory.Checkmark;
        }

    }
}