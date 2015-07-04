using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CafeF.Redis.Data;
using ServiceStack.Redis;

namespace CafeF.Redis.TestUpdate
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
            var id = "58231";
            var compactkey = string.Format(RedisKey.KeyCompanyNewsCompact, id);
            if (redis.ContainsKey(compactkey))
                redis.Remove(compactkey);
            var detailkey = string.Format(RedisKey.KeyCompanyNewsDetail, id);
            if (redis.ContainsKey(detailkey))
                redis.Remove(detailkey);
        }
    }
}
