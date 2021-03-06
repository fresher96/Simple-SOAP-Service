﻿using System;
using System.Windows.Forms;
using ServiceConsumer;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

namespace ServiceConsumer
{
    public partial class Form1 : Form
    {
        private ServiceRef.WebService1 stub;

        public Form1()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = dateTimePicker1.Value.Date;
                var list = stub.AddPerson(textBox1.Text, textBox2.Text, date);
                Populate(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Populate(stub.GetList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Populate(ServiceRef.Person[] list)
        {
            dataGridView1.DataSource = GetDataView(list);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                stub = new ServiceRef.WebService1();
                Populate(stub.GetList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // reflection code
        // from: https://stackoverflow.com/a/564373/4881156
        private DataView GetDataView<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();
            DataView view = new DataView(table);

            // for each property in Person class
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);

                // if `id` exists, sort data based on it in descending order
                if (i == 0 && prop.Name.ToLower() == "id" && prop.PropertyType == typeof(int))
                {
                    view.Sort = string.Format("{0} DESC", prop.Name); // expected "Id DESC"
                }
            }

            // for each Person in the list
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }

            return view;
        }
    }
}
