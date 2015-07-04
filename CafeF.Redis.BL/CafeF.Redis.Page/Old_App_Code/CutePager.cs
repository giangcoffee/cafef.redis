using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
namespace CafeF.Redis.Page
{
    [ToolboxData("<{0}:Pager runat=\"server\"></{0}:Pager>")]
    public class Pager : WebControl, IPostBackEventHandler, INamingContainer {
       
        private static readonly object EventCommand = new object();
        public event CommandEventHandler Command {
            add { Events.AddHandler(EventCommand, value); }
            remove { Events.RemoveHandler(EventCommand, value); }
        }
        protected virtual void OnCommand(CommandEventArgs e) {
            CommandEventHandler clickHandler = (CommandEventHandler)Events[EventCommand];
            if (clickHandler != null) clickHandler(this, e);
        }
        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument) {
            OnCommand(new CommandEventArgs(this.UniqueID, Convert.ToInt32(eventArgument)));
        }

        // private int _currentIndex; // currnet page
        private double _itemCount; // total count of rows
        private int _pageSize = 15; // the Size of items that can be displayed on page
        private int _pageCount; // Total No of Pages
        // private string _PageURLFormat = "ShowResults.aspx?page={0}"; // default value for url format
        private bool _showFirstLast; // to determine wheter show first and last link or not
        [Browsable(false)]
        public double ItemCount {
            get { return _itemCount; }
            set {
                _itemCount = value;

                double divide = ItemCount / PageSize;
                double ceiled = System.Math.Ceiling(divide);
                PageCount = Convert.ToInt32(ceiled);
            }
        }

        [Browsable(false)]
        public int CurrentIndex {
            get {
                if (ViewState["aspnetPagerCurrentIndex"] == null) {
                    ViewState["aspnetPagerCurrentIndex"] = 1;
                    return 1;
                } else {
                    return Convert.ToInt32(ViewState["aspnetPagerCurrentIndex"]);
                }              
            }
            set { ViewState["aspnetPagerCurrentIndex"] = value; }
        }

        [Category("behaviouralSettings")]
        public int PageSize {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        [Browsable(false)]
        public int PageCount {
            get { return _pageCount; }
            set { _pageCount = value; }
        }
        [Category("behaviouralSettings")]
        public bool ShowFirstLast {
            get { return _showFirstLast; }
            set { _showFirstLast = value; }
        }
        // to enable/disable smart shortcuts
        private bool _enableSSC = true;
        [Category("behaviouralSettings")]
        public bool EnableSmartShortCuts {
            get { return _enableSSC; }
            set { _enableSSC = value; }
        }

        // the ration to count the space whithin the smartshortcut pages
        private double _sscRatio = 3.0D;
        [Category("behaviouralSettings")]
        public double SmartShortCutRatio {
            get { return _sscRatio; }
            set { _sscRatio = value; }
        }

        // first compacted group of visible page numbers
        private int _firstCompactedPageCount =10 ;
        [Category("behaviouralSettings")]
        public int CompactedPageCount {
            get { return _firstCompactedPageCount; }
            set { _firstCompactedPageCount = value; }
        }

        // ordinary not compacted visible page numbers count
        private int _notCompactedPageCount = 15;
        [Category("behaviouralSettings")]
        public int NotCompactedPageCount {
            get { return _notCompactedPageCount; }
            set { _notCompactedPageCount = value; }
        }

        // maximum number of smart shortcuts
        private int _maxSmartShortCutCount = 6;
        [Category("behaviouralSettings")]
        public int MaxSmartShortCutCount {
            get { return _maxSmartShortCutCount; }
            set { _maxSmartShortCutCount = value; }
        }

        // the number which determines that the smart short cuts must be rendered if pagecount is morethatn specific number
        private int _sscThreshold = 30;
        [Category("behaviouralSettings")]
        public int SmartShortCutThreshold {
            get { return _sscThreshold; }
            set { _sscThreshold = value; }
        }

        // generate alt title for page indeces
        private bool _altEnabled = true;
        [Category("behaviouralSettings")]
        public bool AlternativeTextEnabled {
            get { return _altEnabled; }
            set { _altEnabled = value; }
        }

      

      
        private string _GO = "go";
        private string _OF = "of";
        private string _FROM = "From";
        private string _PAGE = "Page";
        private string _TO = "to";
        private string _SHOWING_RESULT = "Showing Results";
        private string _SHOW_RESULT = "Show Result";
        private string _BACK_TO_FIRST = "to First Page";
        private string _GO_TO_LAST = "to Last Page";
        private string _BACK_TO_PAGE = "Back to Page";
        private string _NEXT_TO_PAGE = "Next to Page";
        private string _LAST = "&gt;&gt;";
        private string _FIRST = "&lt;&lt;";
        private string _previous = "&lt;";
        private string _next = "&gt;";

        [Category("GlobalizaionSettings")]
        public string GoClause {
            get { return _GO; }
            set { _GO = value; }
        }

        [Category("GlobalizaionSettings")]
        public string OfClause {
            get { return _OF; }
            set { _OF = value; }
        }

        [Category("GlobalizaionSettings")]
        public string FromClause {
            get { return _FROM; }
            set { _FROM = value; }
        }

        [Category("GlobalizaionSettings")]
        public string PageClause {
            get { return _PAGE; }
            set { _PAGE = value; }
        }

        [Category("GlobalizaionSettings")]
        public string ToClause {
            get { return _TO; }
            set { _TO = value; }
        }

        [Category("GlobalizaionSettings")]
        public string ShowingResultClause {
            get { return _SHOWING_RESULT; }
            set { _SHOWING_RESULT = value; }
        }

        [Category("GlobalizaionSettings")]
        public string ShowResultClause {
            get { return _SHOW_RESULT; }
            set { _SHOW_RESULT = value; }
        }

        [Category("GlobalizaionSettings")]
        public string BackToFirstClause {
            get { return _BACK_TO_FIRST; }
            set { _BACK_TO_FIRST = value; }
        }

        [Category("GlobalizaionSettings")]
        public string GoToLastClause {
            get { return _GO_TO_LAST; }
            set { _GO_TO_LAST = value; }
        }

        [Category("GlobalizaionSettings")]
        public string BackToPageClause {
            get { return _BACK_TO_PAGE; }
            set { _BACK_TO_PAGE = value; }
        }

        [Category("GlobalizaionSettings")]
        public string NextToPageClause {
            get { return _NEXT_TO_PAGE; }
            set { _NEXT_TO_PAGE = value; }
        }

        [Category("GlobalizaionSettings")]
        public string LastClause {
            get { return _LAST; }
            set { _LAST = value; }
        }

        [Category("GlobalizaionSettings")]
        public string FirstClause {
            get { return _FIRST; }
            set { _FIRST = value; }
        }

        [Category("GlobalizaionSettings")]
        public string PreviousClause {
            get { return _previous; }
            set { _previous = value; }
        }

        [Category("GlobalizaionSettings")]
        public string NextClause {
            get { return _next; }
            set { _next = value; }
        }

        private bool _rightToLeft = false;
        [Category("GlobalizaionSettings")]
        public bool RTL {
            get { return _rightToLeft; }
            set { _rightToLeft = value; }
        }     
        private string GenerateAltMessage(int desiredPage) {
            StringBuilder altGen = new StringBuilder();
            altGen.Append(desiredPage == CurrentIndex ? ShowingResultClause : ShowResultClause);
            altGen.Append(" ");
            altGen.Append(((desiredPage - 1) * PageSize) + 1);
            altGen.Append(" ");
            altGen.Append(ToClause);
            altGen.Append(" ");
            altGen.Append(desiredPage == PageCount ? ItemCount : desiredPage * PageSize);
            altGen.Append(" ");
            altGen.Append(OfClause);
            altGen.Append(" ");
            altGen.Append(ItemCount);

            return altGen.ToString();
        }

        private string GetAlternativeText(int index) {
            return AlternativeTextEnabled ? string.Format(" title=\"{0}\"", GenerateAltMessage(index)) : "";
        }

        private string RenderFirst() {
            string templateCell = "<td align=\"center\" style=\"width: 20px\"><a  href=\"{0}\" title=\"" + " " + BackToFirstClause + " " + "\"> " + FirstClause + " </a></td>";
            // string templateURL = String.Format(PageURLFormat, "1");
            // return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, "1"));
			return String.Format(templateCell, Page.GetPostBackClientHyperlink(this, "1"));
        }
        private string RenderLast() {
            string templateCell = "<td  align=\"center\" style=\"width: 20px\"><a  href=\"{0}\" title=\"" + " " + GoToLastClause + " " + "\"> " + LastClause + " </a></td>";
            // string templateURL = String.Format(PageURLFormat, PageCount.ToString());
            // return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, PageCount.ToString()));
			return String.Format(templateCell, Page.GetPostBackClientHyperlink(this, PageCount.ToString()));
        }

        private string RenderBack() {
            string templateCell = "<td align=\"center\" style=\"width: 20px\"><a  href=\"{0}\" title=\"" + " " + BackToPageClause + " " + (CurrentIndex - 1).ToString() + "\"> " + PreviousClause + " </a></td>";
            // string templateURL = String.Format(PageURLFormat, (CurrentIndex - 1).ToString());
            return String.Format(templateCell, Page.GetPostBackClientHyperlink(this, (CurrentIndex - 1).ToString()));
        }

        private string RenderNext() {
            string templateCell = "<td align=\"center\" style=\"width: 20px\"><a  href=\"{0}\" title=\"" + " " + NextToPageClause + " " + (CurrentIndex + 1).ToString() + "\"> " + NextClause + " </a></td>";
            // string templateURL = String.Format(PageURLFormat, (CurrentIndex + 1).ToString());
            return String.Format(templateCell, Page.GetPostBackClientHyperlink(this, (CurrentIndex + 1).ToString()));
        }

        private string RenderCurrent() {
            // string altMessage = AlternativeTextEnabled ? string.Format(" title=\"{0}\"", GenerateAltMessage(CurrentIndex)) : "";
            return "<td align=\"center\" style=\"width: 20px\"><span  " + GetAlternativeText(CurrentIndex) + " ><strong> " + CurrentIndex.ToString() + " </strong></span></td>";
        }

        private string RenderOther(int index) {
            string templateCell = "<td align=\"center\" style=\"width: 20px\"><a  href=\"{0}\" " + GetAlternativeText(index) + " > " + index.ToString() + " </a></td>";
            // string templateURL = String.Format(PageURLFormat, index.ToString());
            return String.Format(templateCell, Page.GetPostBackClientHyperlink(this, index.ToString()));
        }

        private string RenderSSC(int index) {
            string templateCell = "<td align=\"center\" style=\"width: 20px\"><a  href=\"{0}\" " + GetAlternativeText(index) + " > " + index.ToString() + " </a></td>";
            // string templateURL = String.Format(PageURLFormat, index.ToString());
            return String.Format(templateCell, Page.GetPostBackClientHyperlink(this, index.ToString()));
        }

		
       
		private ArrayList _smartShortCutList;
		private ArrayList SmartShortCutList
		{
			get { return _smartShortCutList; }
			set { _smartShortCutList = value; }
		}

        private void CalculateSmartShortcutAndFillList() {
            // _smartShortCutList = new List<int>();
			_smartShortCutList = new ArrayList();
            double shortCutCount = this.PageCount * SmartShortCutRatio / 100;
            double shortCutCountRounded = System.Math.Round(shortCutCount, 0);
            if (shortCutCountRounded > MaxSmartShortCutCount) shortCutCountRounded = MaxSmartShortCutCount;
            if (shortCutCountRounded == 1) shortCutCountRounded++;

            for (int i = 1; i < shortCutCountRounded + 1; i++) {
                int calculatedValue = (int)(System.Math.Round((this.PageCount * (100 / shortCutCountRounded) * i / 100) * 0.1, 0) * 10);
                if (calculatedValue >= this.PageCount) break;
                SmartShortCutList.Add(calculatedValue);
            }
        }

        private void RenderSmartShortCutByCriteria(int basePageNumber, bool getRightBand, HtmlTextWriter writer) {
            if (IsSmartShortCutAvailable()) {

                // List<int> lstSSC = this.SmartShortCutList;
				ArrayList lstSSC = this.SmartShortCutList;

                int rVal = -1;

                if (getRightBand) {

                    for (int i = 0; i < lstSSC.Count; i++) {

                        if ((int)lstSSC[i] > basePageNumber) {
                            rVal = i;
                            break;
                        }
                    }

                    if (rVal >= 0) {

                        for (int i = rVal; i < lstSSC.Count; i++) {

                            if ((int)lstSSC[i] != basePageNumber) {
                                writer.Write(RenderSSC((int)lstSSC[i]));
                            }
                        }
                    }


                } else if (!getRightBand) {

                    for (int i = 0; i < lstSSC.Count; i++) {

                        if (basePageNumber > (int)lstSSC[i]) {
                            rVal = i;
                        }
                    }

                    if (rVal >= 0) {
                        for (int i = 0; i < rVal + 1; i++) {
                            if ((int)lstSSC[i] != basePageNumber) {
                                writer.Write(RenderSSC((int)lstSSC[i]));
                            }                           
                        }
                    }
                }
            }
        }

		bool IsSmartShortCutAvailable() {
            return this.EnableSmartShortCuts && this.SmartShortCutList != null && this.SmartShortCutList.Count != 0;
        }
     
        protected override void Render(HtmlTextWriter writer) {

            if (Page != null) Page.VerifyRenderingInServerForm(this);
			if (this.ItemCount == 0)
					return;
            if (this.PageCount > this.SmartShortCutThreshold) {

                if (EnableSmartShortCuts) {
                    CalculateSmartShortcutAndFillList();
                }
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "3");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "1");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "CafeF_Paging");
          
			if (RTL) writer.AddAttribute("dir", "rtl");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            //writer.AddAttribute(HtmlTextWriterAttribute.Class, "");
            //writer.RenderBeginTag(HtmlTextWriterTag.Td);
			//writer.RenderEndTag();

            if (ShowFirstLast && CurrentIndex != 1)
                writer.Write(RenderFirst());

//            if (CurrentIndex != 1)
//                writer.Write(RenderBack());
			if (CurrentIndex > 1)
			    writer.Write(RenderBack());


            if (CurrentIndex < CompactedPageCount) 
			{

                if (CompactedPageCount > PageCount) CompactedPageCount = PageCount;

                for (int i = 1; i < CompactedPageCount + 1; i++) {
                    if (i == CurrentIndex) {
                        writer.Write(RenderCurrent());
                    } else {
                        writer.Write(RenderOther(i));
                    }
                }
                RenderSmartShortCutByCriteria(CompactedPageCount, true, writer);


            } else if (CurrentIndex >= CompactedPageCount && CurrentIndex < NotCompactedPageCount) {

                if (NotCompactedPageCount > PageCount) NotCompactedPageCount = PageCount;

                for (int i = 1; i < NotCompactedPageCount + 1; i++) {
                    if (i == CurrentIndex) {
                        writer.Write(RenderCurrent());
                    } else {
                        writer.Write(RenderOther(i));
                    }
                }

                RenderSmartShortCutByCriteria(NotCompactedPageCount, true, writer);

            } else if (CurrentIndex >= NotCompactedPageCount) {
                int gapValue = NotCompactedPageCount / 2;
                int leftBand = CurrentIndex - gapValue;
                int rightBand = CurrentIndex + gapValue;

                RenderSmartShortCutByCriteria(leftBand, false, writer);

                for (int i = leftBand; (i < rightBand + 1) && i < PageCount + 1; i++) {
                    if (i == CurrentIndex) {
                        writer.Write(RenderCurrent());
                    } else {
                        writer.Write(RenderOther(i));
                    }
                }

                if (rightBand < this.PageCount) {
                    RenderSmartShortCutByCriteria(rightBand, true, writer);
                }

            }

            if (CurrentIndex != PageCount)
                writer.Write(RenderNext());

            if (ShowFirstLast && CurrentIndex != PageCount)
                writer.Write(RenderLast());

            writer.RenderEndTag();

            writer.RenderEndTag();

            base.Render(writer);
        }
      

    }
}