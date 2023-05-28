Imports System.Data.OleDb

Public Class InputDataMobil
    Dim conn As OleDbConnection
    Dim da As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand
    Dim str As String

    ' METHOD: Koneksi()
    Sub Koneksi()
        ' Ganti path str sesuai dengan lokasi Connection String database.
        str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\MyData\Project\cpp\Project\InputDataMobil\database\dbmobil.mdb"
        conn = New OleDbConnection(str)
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    ' METHOD: Tampilgrid()
    Sub Tampilgrid()
        da = New OleDbDataAdapter("select * from tb_mobil", conn)
        ds = New DataSet
        da.Fill(ds, "tb_mobil")
        DataGridView1.DataSource = ds.Tables("tb_mobil")
    End Sub

    ' METHOD: InputDataMobil_Load()
    Private Sub InputDataMobil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call Tampilgrid()
        Call TextMati()
        Me.btnTambah.Enabled = True
        Me.btnSimpan.Enabled = False
        Me.btnKeluar.Enabled = True
    End Sub

    ' METHOD: TextMati()
    Sub TextMati()
        Me.tbType.Enabled = False
        Me.tbHarga.Enabled = False
        Me.cbNama.Enabled = False
        Me.cbWarna.Enabled = False
    End Sub

    ' METHOD: TextHidup()
    Sub TextHidup()
        Me.tbType.Enabled = True
        Me.tbHarga.Enabled = True
        Me.cbNama.Enabled = True
        Me.cbWarna.Enabled = True
    End Sub

    ' METHOD: Kosong()
    Sub Kosong()
        tbType.Clear()
        tbHarga.Clear()
    End Sub

    ' BUTTON: Button Tambah
    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        Call Kosong()
        Call TextHidup()
        Me.btnTambah.Enabled = False
        Me.btnSimpan.Enabled = True
        Me.btnKeluar.Enabled = True
    End Sub

    ' BUTTON: Button Simpan
    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If tbType.Text = "" Or cbNama.Text = "" Or tbHarga.Text = "" Or cbWarna.Text = "" Then
            MsgBox("Data belum lengkap, pastikan semua form terisi")
            Exit Sub
        Else
            Call Koneksi()
            Dim simpan As String = "insert into tb_mobil (typeMobil, namaMobil, hargaMobil, warnaMobil)" & "values ('" & tbType.Text & "','" & cbNama.Text & "','" & tbHarga.Text & "','" & cbWarna.Text & "')"
            cmd = New OleDbCommand(simpan, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Data berhasil di input", MsgBoxStyle.Information, "Information")
            Me.OleDbConnection1.Close()
            Call Tampilgrid()
            DataGridView1.Refresh()
            Call Koneksi()
            Call Kosong()
            Call TextMati()
            Me.btnTambah.Enabled = True
            Me.btnSimpan.Enabled = False
            Me.btnKeluar.Enabled = True
        End If
    End Sub
End Class
