namespace SwissDraw
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.AddPerson = new System.Windows.Forms.Button();
            this.MakeMatch = new System.Windows.Forms.Button();
            this.ViewCalc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddPerson
            // 
            this.AddPerson.Location = new System.Drawing.Point(40, 10);
            this.AddPerson.Name = "AddPerson";
            this.AddPerson.Size = new System.Drawing.Size(200, 60);
            this.AddPerson.TabIndex = 0;
            this.AddPerson.Text = "参加者登録";
            this.AddPerson.UseVisualStyleBackColor = true;
            // 
            // MakeMatch
            // 
            this.MakeMatch.Location = new System.Drawing.Point(40, 87);
            this.MakeMatch.Name = "MakeMatch";
            this.MakeMatch.Size = new System.Drawing.Size(200, 60);
            this.MakeMatch.TabIndex = 1;
            this.MakeMatch.Text = "対戦組合せ開始";
            this.MakeMatch.UseVisualStyleBackColor = true;
            // 
            // ViewCalc
            // 
            this.ViewCalc.Location = new System.Drawing.Point(40, 166);
            this.ViewCalc.Name = "ViewCalc";
            this.ViewCalc.Size = new System.Drawing.Size(200, 60);
            this.ViewCalc.TabIndex = 2;
            this.ViewCalc.Text = "点数計算";
            this.ViewCalc.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.ViewCalc);
            this.Controls.Add(this.MakeMatch);
            this.Controls.Add(this.AddPerson);
            this.Name = "Form1";
            this.Text = "対戦表表示システム";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddPerson;
        private System.Windows.Forms.Button MakeMatch;
        private System.Windows.Forms.Button ViewCalc;
    }
}

