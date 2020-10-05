using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commentgroup
{
    public partial class Form1 : Form
    {
        public IWebDriver[] chromeDriver = new IWebDriver[1000];
        public BindingList<ModelHistory>[] history = new BindingList<ModelHistory>[1000];
        public int rowIndexHistory;
        public Form1()
        {
            InitializeComponent();
            CreadFile();
            DocFileImage();
            dgvAccounts.AutoGenerateColumns = false;
            dgvAccounts.DataSource = DocFileTaiKhoan();
        }

        private string UrlFrienfListMFa(string Uid)
        {
            return $"https://m.facebook.com/profile.php?v=friends&lst={Uid}%3A{Uid}%3A1598977239";
        }

        private void CreadFile()
        {
            if (!Directory.Exists("config\\group"))
            {
                Directory.CreateDirectory("config\\group");
            }
            if (!Directory.Exists("config\\image"))
            {
                Directory.CreateDirectory("config\\image");
            }
            if (!File.Exists("config\\accounts.txt"))
            {
                var file = File.Create("config\\accounts.txt");
                file.Close();
            }
            if (!File.Exists("config\\comments.txt"))
            {
                var file = File.Create("config\\comments.txt");
                file.Close();
            }
            if (!File.Exists("config\\uidbaiviet.txt"))
            {
                var file = File.Create("config\\uidbaiviet.txt");
                file.Close();
            }
        }

        private void openAccs_Click(object sender, EventArgs e)
        {
            Process.Start($"{Environment.CurrentDirectory}\\config\\accounts.txt");
        }

        private void openComments_Click(object sender, EventArgs e)
        {
            Process.Start($"{Environment.CurrentDirectory}\\config\\comments.txt");
        }

        private void openImage_Click(object sender, EventArgs e)
        {
            Process.Start($"{Environment.CurrentDirectory}\\config\\image");
        }
        public List<string> DocFileImage()
        {
            var listImage = Directory.GetFiles($"{Environment.CurrentDirectory}\\config\\image");
            return listImage.ToList();
        }

        private void dgvAccounts_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu_dgv = new ContextMenuStrip();
                int positionClick = this.dgvAccounts.HitTest(e.X, e.Y).RowIndex;
                if (positionClick >= 0)
                {
                    menu_dgv.Items.Add("Thêm nhóm").Name = "Thêm nhóm";
                    menu_dgv.Show(dgvAccounts, new Point(e.X, e.Y));
                    if (dgvAccounts.SelectedRows.Count == 1)
                    {
                        dgvAccounts.ClearSelection();
                    }
                    dgvAccounts.Rows[positionClick].Selected = true;
                    menu_dgv.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemChicked);
                }
            }
        }

        private void my_menu_ItemChicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (DataGridViewRow row in dgvAccounts.SelectedRows)
            {
                switch (e.ClickedItem.Name.ToString())
                {
                    case "Thêm nhóm":
                        AddFileNhom(row.Index);
                        break;
                }
            }
        }

        private void AddFileNhom(int rowIndex)
        {
            var id = dgvAccounts.Rows[rowIndex].Cells["id"].Value.ToString();
            if (!File.Exists($"config\\group\\{id}.txt"))
            {
                var file = File.Create($"config\\group\\{id}.txt");
                file.Close();
            }
            Process.Start($"{Environment.CurrentDirectory}\\config\\group\\{id}.txt");
        }

        public BindingList<ModelAccount> DocFileTaiKhoan()
        {
            var reg = new Regex("c_user=\\d{0,}");
            var listAcc = new BindingList<ModelAccount>();
            var accounts = File.ReadAllLines("config\\accounts.txt");
            foreach (var account in accounts)
            {
                var item = account.Split('|');
                if (item.Count() == 5)
                {
                    listAcc.Add(new ModelAccount
                    {
                        name = item[0],
                        id = reg.Match(item[1]).Value.Replace("c_user=", ""),
                        cookie = item[1],
                        stop = bool.Parse(item[2]),
                        an = bool.Parse(item[3]),
                        action = "Bắt đầu",
                        createListFriend = "Tạo mới",
                        isdemnguoc = true
                    });
                }
            }
            return listAcc;
        }
        public List<string> DocFileComment()
        {
            var listComment = new List<string>();
            listComment = File.ReadAllLines("config\\comments.txt").ToList();
            return listComment;
        }

        private void dgvAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                // click Tạm dừng
                if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
                {
                    if ((bool)this.dgvAccounts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                    {
                        this.dgvAccounts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                    }
                    else
                    {
                        // xử lý code
                        this.dgvAccounts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                    }
                }
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    if (this.dgvAccounts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Bắt đầu")
                    {
                        ChayComment(e.RowIndex);
                        this.dgvAccounts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Kết thúc";
                    }
                    else
                    {
                        // xử lý code
                        this.dgvAccounts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Bắt đầu";
                    }
                }
                if (e.ColumnIndex == 7)
                {
                    if (this.dgvAccounts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Tạo mới")
                    {
                        TaoDSBanBe(e.RowIndex);
                        this.dgvAccounts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Đang tạo";
                    }
                    else
                    {
                        // xử lý code
                        this.dgvAccounts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Tạo mới";
                    }
                }
            }
        }

        private void dgvAccounts_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.rowIndexHistory = e.RowIndex;
            PatchValueHistory(e.RowIndex);
        }
        private void PatchValueHistory(int rowIndex)
        {
            dgvHistory.Invoke(new Action(() =>
            {
                dgvHistory.DataSource = null;
                this.dgvHistory.Rows.Clear();
                dgvHistory.DataSource = history[rowIndex];
            }));
        }
        public string LoginFacebook(IWebDriver chromeDriver, int rowIndex)
        {
            dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Đi đăng nhập Facebook";
            chromeDriver.Url = "https://facebook.com/login";
            Task.Delay(1000);
            if (GetCookieFb(chromeDriver).FirstOrDefault(x => x.Name == "c_user") != null)
            {
                dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Đăng nhập faceBook thành công";
                return GetCookieFb(chromeDriver).FirstOrDefault(x => x.Name == "c_user").Value;
            }
            else
            {
                if (dgvAccounts.Rows[rowIndex].Cells["cookie"].Value != null)
                {
                    dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Đăng nhập Fb bằng cookie";
                    var uid = LoginWithCookie(dgvAccounts.Rows[rowIndex].Cells["cookie"].Value.ToString(), chromeDriver);
                    if (uid != null)
                    {
                        dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Đăng nhập faceBook thành công";
                        return GetCookieFb(chromeDriver).FirstOrDefault(x => x.Name == "c_user").Value;
                    }
                }
                else
                {
                }
            }
            dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Đăng nhập faceBook thất bại, cookie die";
            return null;
        }
        public ReadOnlyCollection<Cookie> GetCookieFb(IWebDriver chromeDriver)
        {
            var cookie = chromeDriver.Manage().Cookies.AllCookies;
            //Cookie listCookie = cookie.Where(x=>x.sp)
            var str = "";
            foreach (var item in cookie)
            {
                str += item.ToString().Split(';')[0] + ";";
            }
            return cookie;
        }
        public string LoginWithCookie(string cookies, IWebDriver chromeDriver)
        {
            chromeDriver.Url = "https://m.facebook.com/home.php";
            cookies = cookies.Replace(" ", "");
            foreach (string item in cookies.Split(';'))
            {
                if (item.Split('=').Count() == 2)
                {
                    chromeDriver.Manage().Cookies.AddCookie(new Cookie(item.Split('=')[0], item.Split('=')[1]));
                }
            }
            chromeDriver.Url = "https://m.facebook.com/home.php";
            Task.Delay(1000);
            var cookieUid = GetCookieFb(chromeDriver).FirstOrDefault(x => x.Name == "c_user");
            if (cookieUid != null)
            {
                return cookieUid.Value;
            }
            else
            {
                return null;
            }
        }
        public bool SetUpChrome(ref ChromeDriverService chromeDriverService, ref ChromeOptions chromeOptions, int rowIndex)
        {
            chromeDriverService.SuppressInitialDiagnosticInformation = true;
            chromeDriverService.HideCommandPromptWindow = true;
            if ((bool)this.dgvAccounts.Rows[rowIndex].Cells["An"].Value)
            {
                chromeOptions.AddArgument("--headless");
            }
            chromeOptions.AddArguments(new string[]
            {
                    "--disable-blink-features=AutomationControlled"
            });
            chromeOptions.AddArgument($"--user-agent= Mozilla/5.0 (Linux; Android 10; SM-G975U) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.93 Mobile Safari/537.36");
            try
            {
                chromeOptions.AddArgument("no-sandbox");
                chromeOptions.AddArguments(new string[]
                {
                    "--disable-notifications"
                });
                chromeOptions.AddArguments(new string[]
                {
                    "--disable-popup-blocking"
                });
                chromeOptions.AddArguments(new string[]
                {
                    "--disable-geolocation"
                });
                chromeOptions.AddArguments(new string[]
                {
                    "--no-sandbox"
                });
                chromeOptions.AddArguments(new string[]
                {
                    "--disable-gpu"
                });
                CheckAndAddProfile(ref chromeOptions, rowIndex);
                try
                {
                    chromeDriver[rowIndex] = new ChromeDriver(chromeDriverService, chromeOptions);
                    chromeDriver[rowIndex].Manage().Window.Size = new Size(400, 850);
                }
                catch (Exception e)
                {
                    dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Hãy update chromedrive mới, hoặc trình duyệt cùng profile đang bật tắt nó đi";
                    dgvAccounts.Rows[rowIndex].Cells["Action"].Value = "Bắt đầu";
                    return false;
                }
                chromeDriver[rowIndex].Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                return true;
            }
            catch (Exception)
            {
                this.dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Có lỗi khi add arguments, tắt đi chạy lại";
            }
            return false;
        }

        private void CheckAndAddProfile(ref ChromeOptions chromeOptions, int rowIndex)
        {
            if (!Directory.Exists("Profile"))
            {
                Directory.CreateDirectory("Profile");
            }
            if (Directory.Exists("Profile"))
            {
                chromeOptions.AddArguments("user-data-dir=" + "Profile" + "\\" + this.dgvAccounts.Rows[rowIndex].Cells["id"].Value);
            }
        }
        private void Reload_Click(object sender, EventArgs e)
        {
            dgvAccounts.DataSource = DocFileTaiKhoan();
        }

        private List<string> GetListGroup(int rowIndex)
        {
            var id = dgvAccounts.Rows[rowIndex].Cells["id"].Value.ToString();
            if (!File.Exists($"config\\group\\{id}.txt"))
            {
                var file = File.Create($"config\\group\\{id}.txt");
                file.Close();
            }
            var listGroup = File.ReadAllLines($"config\\group\\{id}.txt").ToList();
            return listGroup;
        }

        public List<string> GetLinkComment(IWebDriver chromeDriver, string urlGroup)
        {
            ReadOnlyCollection<IWebElement> binhluan;
            IJavaScriptExecutor js = (IJavaScriptExecutor)chromeDriver;
            var listHref = new List<string>();

            chromeDriver.Url = urlGroup;
            int dem = 5;
            while (dem > 0)
            {
                js.ExecuteScript("window.scrollBy(0,700)");
                dem--;
                Thread.Sleep(500);
            }
            try
            {
                binhluan = chromeDriver.FindElements(By.XPath("//a[text()='Bình luận']"));
            }
            catch
            {
                return listHref;
            }
            var listUidIgnore = new List<int>();
            var list = File.ReadAllLines("config\\uidbaiviet.txt");
            for (int i = 0; i < binhluan.Count(); i++)
            {
                var id = new Regex(@"&id=\d{1,}").Match(binhluan[i].GetAttribute("href")).Value.Replace("&id=", "");
                if (list.Contains(id))
                {
                    listUidIgnore.Add(i);
                }
            }

            var listnumber = new List<int>();
            for (int i = 0; i < binhluan.Count; i++)
            {
                listnumber.Add(i);
            }

            var listnumsecomment = listnumber.RandomList(Convert.ToInt32(this.numberComment.Value), listUidIgnore);
            foreach (var number in listnumsecomment)
            {
                listHref.Add(binhluan[number].GetAttribute("href"));
            }

            return listHref;
        }

        private void ChayComment(int rowIndex)
        {
            Task t = new Task(() =>
            {
                ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                ChromeOptions chromeOptions = new ChromeOptions();
                if (!SetUpChrome(ref chromeDriverService, ref chromeOptions, rowIndex)) return;
                LoginFacebook(chromeDriver[rowIndex], rowIndex);
                var listComment = DocFileComment();
                var listimage = DocFileImage();
                if (listComment == null)
                {
                    dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Phải chưa cấu hình commnet";
                    return;
                }
                var listGroups = GetListGroup(rowIndex);
                if (listGroups == null)
                {
                    dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Bạn chưa thêm nhóm nào";
                    return;
                }
                var reg = new Regex(@"groups\/\d{1,}");
                try
                {
                    foreach (var linkGroup in listGroups)
                    {
                        var listLinkComment = GetLinkComment(chromeDriver[rowIndex], linkGroup);
                        if (listLinkComment != null)
                        {
                            foreach (var linkCommet in listLinkComment)
                            {
                                CheckStop(rowIndex);
                                dgvAccounts.Rows[rowIndex].Cells["status"].Value = $"đi Comment bài {reg.Match(linkCommet).Value.Replace("groups/", "")}";
                                var randomCm = Common.RandomValue(0, listComment.Count() - 1);
                                string linkanh = null;
                                if (cmimage.Checked == true)
                                {
                                    var randomimg = Common.RandomValue(0, listimage.Count());
                                    linkanh = listimage[randomimg];
                                }
                                Thread.Sleep(1000);
                                var commentThanhcong = MComment(chromeDriver[rowIndex], rowIndex, linkCommet, listComment[randomCm], linkanh);

                                #region demnguocthaotac
                                if (commentThanhcong)
                                {
                                    DoiLamJobKhac(rowIndex);
                                }
                                #endregion

                            }
                        }
                        else
                        {
                            dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Nhóm không có nút bình luận";
                        }
                    }
                    dgvAccounts.Rows[rowIndex].Cells["status"].Value = $"Hoàn thành công việc!";
                    chromeDriver[rowIndex].Quit();
                }
                catch (Exception e)
                {
                    dgvAccounts.Rows[rowIndex].Cells["status"].Value = $"Error: báo lại lỗi này nhé {e.ToString()}";
                }
            });
            t.Start();
        }

        public bool MComment(IWebDriver chromeDriver, int rowIndex, string url, string comment, string linkimage = null)
        {
            try
            {
                chromeDriver.Url = url;
                chromeDriver.FindElement(By.XPath("//a[text()='Bình luận']")).Click();
                chromeDriver.FindElement(By.XPath("//textarea[@id='composerInput']")).SendKeys($"{comment}");
                if (linkimage != null)
                {
                    chromeDriver.FindElement(By.XPath("//input[@accept='image/*, image/heif, image/heic']")).SendKeys($"{linkimage}");
                }
                Thread.Sleep(5000);
                chromeDriver.FindElement(By.XPath("//button[@aria-label='Đăng']")).Click();
                dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Comment thành công";
                if (history[rowIndex] == null)
                {
                    history[rowIndex] = new BindingList<ModelHistory>();
                }
                this.dgvHistory.Invoke(new Action(() =>
                {
                    history[rowIndex].Add(new ModelHistory
                    {
                        stt = history[rowIndex].Count() + 1,
                        time = DateTime.Now,
                        status = "Comment thành công",
                        link = (new Regex(@".{1,}&focus")).Match(chromeDriver.Url).Value.Replace("&focus", "").Replace("//m.f", "//f").ToString()
                    });
                }));

                var id = new Regex(@"id=\d{1,}").Match(chromeDriver.Url).Value.Replace("id=", "");
                File.AppendAllLines("config\\uidbaiviet.txt", new string[] { id });
                return true;
            }
            catch (Exception e)
            {
                if (history[rowIndex] == null)
                {
                    history[rowIndex] = new BindingList<ModelHistory>();
                }
                this.dgvHistory.Invoke(new Action(() =>
                {
                    history[rowIndex].Add(new ModelHistory
                    {
                        stt = history[rowIndex].Count() + 1,
                        time = DateTime.Now,
                        status = "Comment thất bại!",
                        link = (new Regex(@".{1,}&focus")).Match(chromeDriver.Url).Value.Replace("&focus", "").ToString()
                    });
                }));
                dgvAccounts.Rows[rowIndex].Cells["status"].Value = "Comment thất bại!";
                return false;
            }
            // báo lỗi
        }

        private bool DoiLamJobKhac(int rowIndex)
        {
            Task wait = new Task(() =>
            {
                ChoClickButtonFB(rowIndex, "Làm comment tiếp theo");
            });
            wait.Start();

            Task lamvieckhac = new Task(() =>
            {
                while (!(wait.Status == TaskStatus.RanToCompletion))
                {
                    //đi làm việc khác
                    var ramdom = new Random().Next(1, 6);
                    if (ramdom % 2 == 0 || ramdom % 3 == 0)
                    {
                        BoQuaDemNguoc(rowIndex);
                        TrithongMinh(rowIndex, chromeDriver[rowIndex]);
                        TiepTucDemNguoc(rowIndex);
                    }
                    Thread.Sleep(5000);
                }
            });
            lamvieckhac.Start();
            Task.WaitAll(new Task[] { lamvieckhac });
            return true;
        }



        public void ChoClickButtonFB(int rowIndex, string nameJob = "thao tác")
        {
            var randomTime = (new Random()).Next(Convert.ToInt32(delayfrom.Value), Convert.ToInt32(delayto.Value));
            while (randomTime > 0)
            {
                if (LayGiaTriDemNguoc(rowIndex))
                {
                    dgvAccounts.Rows[rowIndex].Cells["status"].Value = $"{nameJob} sau {randomTime} giây";
                }
                Thread.Sleep(1000);
                randomTime--;
            }
        }

        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var url = dgvHistory.Rows[e.RowIndex].Cells["link"].Value.ToString();
                Process.Start(url);
            }
        }

        private void CheckStop(int rowIndex)
        {
            while ((bool)dgvAccounts.Rows[rowIndex].Cells["stop"].Value == true)
            {
                dgvAccounts.Rows[rowIndex].Cells["status"].Value = $"Đang tạm dừng làm việc";
                Thread.Sleep(1000);
            }
            dgvAccounts.Rows[rowIndex].Cells["status"].Value = $"Huỷ bỏ tạm dừng tiếp tục làm việc";
        }

        private void BoQuaDemNguoc(int rowIndex)
        {
            dgvAccounts["isdemnguoc", rowIndex].Value = false;
        }
        private void TiepTucDemNguoc(int rowIndex)
        {
            dgvAccounts["isdemnguoc", rowIndex].Value = true;
        }
        private bool LayGiaTriDemNguoc(int rowIndex)
        {
            return (bool)dgvAccounts["isdemnguoc", rowIndex].Value;
        }

        private void TrithongMinh(int rowIndex, IWebDriver chromeDriver)
        {
            var uid = dgvAccounts["id", rowIndex].Value.ToString();
            var ramdom = new Random().Next(1, 30);
            int[] luotnewfeed = { 1, 2, 3, 4, 5, 6, };
            int[] like = { 8, 9, 10, 11 };
            int[] camxuc = { 12, 13, 14 };
            int[] xemthongbao = { 14, 16 };
            int[] tuongtacprofilebanbe = { 19, 17, 18, 26, 27, 28, 29, 30, 23, 25, 24, 7 };
            int[] xemgroup = { 20, 21 };

            if (chromeDriver.Url != "https://m.facebook.com/home.php")
            {
                chromeDriver.Navigate().Back();
                if (chromeDriver.Url != "https://m.facebook.com/home.php")
                {
                    chromeDriver.Url = "https://m.facebook.com/home.php";
                }
            }
            Thread.Sleep(1000);

            if (luotnewfeed.Contains(ramdom))
            {
                dgvAccounts["status", rowIndex].Value = "Tự động lướt newfeed";
                MActionLuotNewFeed(chromeDriver);
            }
            else if (like.Contains(ramdom))
            {
                dgvAccounts["status", rowIndex].Value = "Tự động Like ngẫu nhiên bài viết trên tường";
                MActionLikePost(chromeDriver);
            }
            else if (camxuc.Contains(ramdom))
            {
                var ramCX = new Random().Next(0, Enum.GetNames(typeof(ActionNuoiFbEnum)).Length);
                dgvAccounts["status", rowIndex].Value = "Tự động thả cảm xúc ngẫu nhiên bài viết trên tường";
                MActionCamXuc(chromeDriver, (ActionNuoiFbEnum)ramCX);
            }
            else if (xemthongbao.Contains(ramdom))
            {
                MCheckThongBao(chromeDriver, rowIndex);
            }
            else if (tuongtacprofilebanbe.Contains(ramdom))
            {
                var num = File.ReadAllLines($"config\\{uid}\\banbedatuongtac.txt").Count();
                var num2 = File.ReadAllLines($"config\\{uid}\\banbe.txt");
                var uidtuongtac = num2[num];
                if (num == num2.Count())
                {
                    File.WriteAllText($"config\\{uid}\\banbedatuongtac.txt", "");
                    uidtuongtac = num2[0];
                }
                File.AppendAllLines($"config\\{uid}\\banbedatuongtac.txt", new string[] { uidtuongtac });
                MActionTuongTacViewProfile(chromeDriver, rowIndex, uidtuongtac);
            }
            else if (xemgroup.Contains(ramdom))
            {
                MActionViewGroup(chromeDriver, rowIndex);
            }
        }

        private bool MActionTuongTacViewProfile(IWebDriver chromeDriver, int rowIndex, string uidtuongtac)
        {
            try
            {
                dgvAccounts["status", rowIndex].Value = $"Tự động tương tác Profile của {uidtuongtac}";
                chromeDriver.Url = $"https://m.facebook.com/{uidtuongtac}";
                var i = 0;
                while (i < 6)
                {
                    var ramdom = new Random().Next(1, 28);
                    if (ramdom % 3 == 0)
                    {
                        var ramCX = new Random().Next(0, Enum.GetNames(typeof(ActionNuoiFbEnum)).Length);
                        dgvAccounts["status", rowIndex].Value = "Tự động thả cảm xúc ngẫu nhiên bài viết profile";
                        MActionCamXuc(chromeDriver, (ActionNuoiFbEnum)ramCX);
                    }
                    else if (ramdom % 2 == 0)
                    {
                        dgvAccounts["status", rowIndex].Value = "Tự động Like ngẫu nhiên bài viết profile";
                        MActionLikePost(chromeDriver);
                    }
                    i++;
                }
                return true;
            }
            catch (Exception)
            {
            }
            return false;
            // báo lỗi
        }
        private bool MActionViewGroup(IWebDriver chromeDriver, int rowIndex)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)chromeDriver;
                var profiles = chromeDriver.FindElements(By.XPath("//a[contains(@href,'/groups/') and contains(@href,'C-R')]"));
                var index = Common.RandomValue(0, profiles.Count() - 1);
                dgvAccounts["status", rowIndex].Value = $"Tự động xem group: {profiles[index].Text}";
                profiles[index].Click();
                Thread.Sleep(1500);
                var i = new Random().Next(3, 7);
                while (i > 0)
                {
                    js.ExecuteScript("window.scrollBy(0,700)");
                    Thread.Sleep(1700);
                    i--;
                }
                return true;
            }
            catch (Exception)
            {
            }
            return false;
            // báo lỗi
        }
        private bool MActionLuotNewFeed(IWebDriver chromeDriver)
        {
            try
            {
                var i = new Random().Next(2, 7);
                while (i > 0)
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)chromeDriver;
                    js.ExecuteScript("window.scrollBy(0,700)");
                    i--;
                    Thread.Sleep(1500);
                }
                return true;
            }
            catch (Exception)
            {
            }
            return false;
            // báo lỗi
        }
        private bool MCheckThongBao(IWebDriver chromeDriver, int rowIndex)
        {
            dgvAccounts["status", rowIndex].Value = "Tự động kiểm tra thông báo";
            try
            {
                var like = chromeDriver.FindElement(By.XPath("//div[@id='notifications_jewel']"));
                like.Click();
                Thread.Sleep(2500);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
            // báo lỗi
        }
        private bool MActionLikePost(IWebDriver chromeDriver)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)chromeDriver;
                var likes = chromeDriver.FindElements(By.XPath("//a[@class='_15ko _77li touchable']"));
                var index = Common.RandomValue(0, likes.Count());
                ((IJavaScriptExecutor)chromeDriver).ExecuteScript("arguments[0].scrollIntoView({block: \"center\",inline: \"center\",behavior: \"smooth\"});", likes[index]);
                Thread.Sleep(1500);
                likes[index].Click();
                Thread.Sleep(500);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
            // báo lỗi
        }
        private bool MActionCamXuc(IWebDriver chromeDriver, ActionNuoiFbEnum actionFb)
        {
            try
            {
                var likes = chromeDriver.FindElements(By.ClassName("_1ekf"));
                var index = Common.RandomValue(0, likes.Count());
                IJavaScriptExecutor js = (IJavaScriptExecutor)chromeDriver;
                ((IJavaScriptExecutor)chromeDriver).ExecuteScript("arguments[0].scrollIntoView({block: \"center\",inline: \"center\",behavior: \"smooth\"});", likes[index]);
                Thread.Sleep(1000);
                js.ExecuteScript($"var a = document.getElementsByClassName('_1ekf'); a[{index}].click();");
                switch (actionFb)
                {
                    case ActionNuoiFbEnum.Love:
                        Thread.Sleep(1000);
                        chromeDriver.FindElement(By.XPath("//div[@aria-label='Yêu thích']")).Click();
                        break;
                    case ActionNuoiFbEnum.Care:
                        Thread.Sleep(1000);
                        chromeDriver.FindElement(By.XPath("//div[@aria-label='Thương thương']")).Click();
                        break;
                    case ActionNuoiFbEnum.Wow:
                        Thread.Sleep(1000);
                        chromeDriver.FindElement(By.XPath("//div[@aria-label='Wow']")).Click();
                        break;
                    case ActionNuoiFbEnum.Haha:
                        Thread.Sleep(1000);
                        chromeDriver.FindElement(By.XPath("//div[@aria-label='Haha']")).Click();
                        break;
                    default:
                        return false;
                }
                Thread.Sleep(1000);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
            // báo lỗi
        }

        private void cmimage_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.cmimage.Checked == true)
            //    {
            //        this.cmimage.Invoke(new MethodInvoker(delegate ()
            //        {
            //            this.cmimage.Checked = false;
            //        }));
            //    }
            //    else
            //    {
            //        this.cmimage.Invoke(new MethodInvoker(delegate ()
            //        {
            //            this.cmimage.Checked = true;
            //        }));
            //    }
            //}
            //catch (Exception c)
            //{

            //}
        }

        private List<string> GetUidFriend(IWebDriver chromeDriver, string uid)
        {
            chromeDriver.Url = UrlFrienfListMFa(uid);
            IJavaScriptExecutor js = (IJavaScriptExecutor)chromeDriver;
            while (FindExitsElement(chromeDriver, By.XPath("//div[@class='seeMoreFriends acw apl']")))
            {
                for (int i = 0; i < 4; i++)
                {
                    js.ExecuteScript("window.scrollBy(0,700)");
                }
            }

            var a = chromeDriver.PageSource;
            var listUid = new List<string>();

            var listbanbe = new Regex("data-store=\"{&quot;id&quot;:\\d{4,},&quot;ref_param").Matches(a);
            foreach (Match item in listbanbe)
            {
                var id = item.Value.Replace("data-store=\"{&quot;id&quot;:", "").Replace(",&quot;ref_param", "");
                listUid.Add(id);
            }
            return listUid;
        }

        private bool FindExitsElement(IWebDriver chromeDriver, By by)
        {
            try
            {
                Thread.Sleep(500);
                chromeDriver.FindElement(by);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void TaoDSBanBe(int rowIndex)
        {
            var uid = dgvAccounts["id", rowIndex].Value.ToString();
            if (!Directory.Exists($"config\\{uid}"))
            {
                Directory.CreateDirectory($"config\\{uid}");
            }
            if (!File.Exists($"config\\{uid}\\banbe.txt"))
            {
                var file = File.Create($"config\\{uid}\\banbe.txt");
                file.Close();
            }
            if (!File.Exists($"config\\{uid}\\banbedatuongtac.txt"))
            {
                var file = File.Create($"config\\{uid}\\banbedatuongtac.txt");
                file.Close();
            }
            Task t = new Task(() =>
            {
                ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                ChromeOptions chromeOptions = new ChromeOptions();
                if (!SetUpChrome(ref chromeDriverService, ref chromeOptions, rowIndex)) return;
                LoginFacebook(chromeDriver[rowIndex], rowIndex);
                this.dgvAccounts["status", rowIndex].Value = "Đang tạo danh sách bạn bè";
                var list = GetUidFriend(chromeDriver[rowIndex], uid);
                File.WriteAllText($"config\\{uid}\\banbe.txt", "");
                File.WriteAllLines($"config\\{uid}\\banbe.txt", list);
                this.dgvAccounts["createListFriend", rowIndex].Value = "Hoàn thành";
                this.dgvAccounts["status", rowIndex].Value = "Tạo danh sách bạn bè thành công";
            });
            t.Start();
        }

    }


    public class ModelAccount
    {
        public string name { get; set; }
        public string id { get; set; }
        public string cookie { get; set; }
        public bool stop { get; set; }
        public bool an { get; set; }
        public bool isdemnguoc { get; set; }
        public string action { get; set; }
        public string createListFriend { get; set; }
    }
    public class ModelHistory
    {
        public int stt { get; set; }
        public DateTime time { get; set; }
        public string status { get; set; }
        public string link { get; set; }
    }

    public enum ActionNuoiFbEnum
    {
        [Display(Name = "Love")]
        Love,
        [Display(Name = "Care")]
        Care,
        [Display(Name = "Wow")]
        Wow,
        [Display(Name = "Haha")]
        Haha,
    }

    public enum ActionFb
    {
        Love = 1,
        Care = 2,
        Wow = 3
    }
}
