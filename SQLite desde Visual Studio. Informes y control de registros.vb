'--- Código para gestión de tablas con SQLite desde VB.net. 
'--- Editado con Visual Studio 2013.
'--- Para el software PWP
'--- Fecha 10-12-2014 por Oscar de la Cuesta
'--- www.palentino.es
Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports System.Drawing
'--- OTRAS LIBRERIAS UTILES
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Xml
'-- Para trabajar con SQlite
Imports System.Data.SQLite
Imports System.Data

Public Class CCclientes

    'IMPORTNTE, Variable para el tipo de tabla a gestionar
    Dim tabla = "clientes"
    Dim Nombre_Base = "Pwp-base.s3db"
	
    Private bs As New BindingSource()
	
    Private dataAdapter As New SQLiteConnection
    Private apd
    Dim ruta, cadena As String
    Dim tab As String = tabla
    Dim conexion = New SQLiteConnection(cadena)
    Dim cmd = New SQLiteCommand("SELECT * FROM " & tabla, conexion)
    Dim adapter = New SQLiteDataAdapter(cmd)
    Dim dt As DataTable = Nothing
    Dim ds As New DataSet
	
    Private Sub Contactos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
	
        labeltabla.Text = "Tabla " & tabla
		
        DataGridView1.EditMode = DataGridViewEditMode.EditOnEnter
		
        ruta = Application.StartupPath + "\" & Nombre_Base
        cadena = "Data Source=" & Application.StartupPath & "\" & Nombre_Base
		
        Dim conexion = New SQLiteConnection(cadena)
        Dim cmd = New SQLiteCommand("SELECT * FROM " & tabla, conexion)
        Dim adapter = New SQLiteDataAdapter(cmd)
        Dim dt As DataTable = Nothing
        Dim ds As New DataSet
		
        Try
            adapter.Fill(DataSet1, tabla)
            adapter.Fill(ds, tabla)
            dt = ds.Tables(tabla)
        Catch ex As Exception
            MsgBox("Se ha cancelado la acción: " & ex.Message)
        End Try
		
        'Haciendo un binding  
        bs.DataSource = adapter
		
        Me.DataGridView1.DataSource = DataSet1
        Me.DataGridView1.DataMember = tab
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        DataGridView1.DefaultCellStyle.BackColor = Color.Beige
		
        Try
            estado.Text = "Tiene " & CStr(dt.Rows.Count) & " " & tabla & "."
        Catch ex As Exception
            MsgBox("Error en la cuenta de " & tabla & " en la base: " & ex.Message)
        Finally
            If Not cmd Is Nothing Then cmd.Dispose()
            DataGridView1.Refresh()
        End Try
    End Sub
	
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
	
        ID.Text = ""
        empresa.Text = ""
		
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        'El id
        If Not String.IsNullOrEmpty(DataGridView1.Item(0, i).Value.ToString) Then
            ID.Text = DataGridView1.Item(0, i).Value
        End If
        'la empresa
        If Not String.IsNullOrEmpty(DataGridView1.Item(1, i).Value.ToString) Then
            empresa.Text = DataGridView1.Item(1, i).Value
        End If
        'Otros ejemplos, OJO cambiar tambien en el evento de cambio de celda del datagrid
        'Apellidos
        '  If Not String.IsNullOrEmpty(DataGridView1.Item(2, i).Value.ToString) Then
        'Iautor.Text = DataGridView1.Item(2, i).Value
        ' End If
        'Email
        '  If Not String.IsNullOrEmpty(DataGridView1.Item(3, i).Value.ToString) Then
        'Ifecha.Text = DataGridView1.Item(3, i).Value
        '  End If
    End Sub
	
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Guardar.Click
        Try
            BNuevo.Visible = True
            BNuevo.Enabled = True
            Borrar.Enabled = True
            Modificar.Enabled = True
            Ncancel.Visible = False
			
            Dim conexion As New SQLite.SQLiteConnection()
			
            conexion.ConnectionString = cadena
            conexion.Open()
            cmd = conexion.CreateCommand
			
            cmd.CommandText = "INSERT INTO " & tabla & "(id, empresa) " & _
            "values(" & ID.Text & "," & _
            "'" & empresa.Text & "'" & _
            ");"
			
            cmd.ExecuteNonQuery()
			
            estado.Text = "Estado: Datos almacenados correctamente."
			
            Modificar.Enabled = True
            Guardar.Enabled = False
			
            'Refrescamos el control superior.
            Me.refrescar_Click(sender, New System.EventArgs())
            DataGridView1.Refresh()
			
            'Inicializamos los datos.
            ID.Text = ""
            empresa.Text = ""
			
            DataGridView1.CurrentCell = DataGridView1.Rows(CInt(ID.Text) - 1).Cells(0)
			
        Catch ex As Exception
            estado.Text = "Error en la inserción : " & ex.Message
        Finally
        End Try
    End Sub
	
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Borrar.Click
        Dim salida As Integer
        Dim conexion As New SQLite.SQLiteConnection()
        conexion.ConnectionString = cadena
        conexion.Open()
        cmd = conexion.CreateCommand
        If ID.Text <> "" Then
            salida = MsgBox("¿ Está seguro de realizar esta acción ?, Seleccionado: " & ID.Text, MsgBoxStyle.YesNo)
        End If
        If salida = 6 Then
            Try
                cmd.CommandText = "DELETE FROM " & tabla & " WHERE ID = " & ID.Text
                cmd.ExecuteNonQuery()
                'Refrescamos el control superior.
                DataGridView1.Refresh()
                Me.refrescar_Click(sender, New System.EventArgs())
                estado.Text = "Estado: El registro se ha eliminado correctamente."
                ID.Text = ""
                empresa.Text = ""
            Catch ex As Exception
                MsgBox("Error en el borrado " & ex.Message)
            End Try
        End If
    End Sub
	
    Private Sub refrescar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles refrescar.Click
        Dim dt As DataTable = Nothing
        Dim ds As New DataSet
        ruta = Application.StartupPath + "\" & Nombre_Base
        cadena = "Data Source=" & Application.StartupPath & "\" & Nombre_Base
        Dim conexion = New SQLiteConnection(cadena)
        Dim cmd = New SQLiteCommand("SELECT * FROM " & tabla, conexion)
        Dim adapter = New SQLiteDataAdapter(cmd)
        Try
            adapter.MissingMappingAction = MissingMappingAction.Passthrough
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
        Catch ex As Exception
            MsgBox("Error en la conexion con la base: " & ex.Message)
        End Try
		
        For Each tbl As DataTable In DataSet1.Tables
            tbl.Clear()
        Next
		
        'Rellenar la tabla ahora
        Try
            adapter.Fill(DataSet1, tab)
            adapter.Fill(ds, tabla)
            dt = ds.Tables(tabla)
        Catch ex As Exception
            MsgBox("Fallo al Rellenar: " & ex.Message)
        End Try
		
        'Haciendo un binding ahora 
        bs.DataSource = adapter
		
        Me.DataGridView1.DataSource = DataSet1
        Me.DataGridView1.DataMember = tab
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        DataGridView1.DefaultCellStyle.BackColor = Color.Beige
        DataGridView1.Refresh()
        DataGridView1.Update()
		
        'Contamos de nuevo los contactos.
        Try
            estado.Text = "Tiene " & CStr(dt.Rows.Count) & " registros en la tabla " & tabla
        Catch ex As Exception
		
        Finally
		
        End Try
    End Sub
	
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modificar.Click
        Dim conexion As New SQLite.SQLiteConnection()
        'Dim Micomando As SQLiteCommand
        conexion.ConnectionString = cadena
        conexion.Open()
        cmd = conexion.CreateCommand
		
        Dim i As Integer
        Try
            i = DataGridView1.CurrentRow.Index
            'Add Items in the table
            Dim sql As String = "UPDATE " & tabla & " SET " & _
            "empresa = '" & empresa.Text & "' " & _
            "WHERE (id = " & DataGridView1.Item(0, i).Value & ")"
			
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()
			
            'Refrescamos el control superior.
            DataGridView1.Refresh()
            Me.refrescar_Click(sender, New System.EventArgs())
            estado.Text = "Estado: Datos modificados correctamente."
            DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
		
        End Try
    End Sub
	
    Private Sub BNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BNuevo.Click
	
        Ncancel.Visible = True
        Borrar.Enabled = False
        ID.Text = ""
        empresa.Text = ""
		
        Try
            Dim conexion As New SQLite.SQLiteConnection()
            conexion.ConnectionString = cadena
            conexion.Open()
            cmd = conexion.CreateCommand
			
            Dim numero As Object
            cmd.CommandText = "Select Max(id) as cuenta from " & tabla
            numero = cmd.ExecuteScalar()
			
            If ((IsDBNull(numero))) Then
                ID.Text = "1"
            Else
                If numero = 0 Then
                    ID.Text = "1"
                Else
                    ID.Text = CStr(numero + 1)
                End If
            End If
			
            Guardar.Enabled = True
            Modificar.Enabled = False
            estado.Text = "Estado: Preparando campos para el alta de un nuevo registro."
            BNuevo.Enabled = False
			
        Catch ex As Exception
            MsgBox("Error en la inserción: " & ex.Message)
        Finally
		
        End Try
    End Sub
	
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim salida As Integer
        salida = MsgBox("¿ Está seguro de realizar esta acción ?, Borrará todos los registros: " & ID.Text, MsgBoxStyle.YesNo)
        If salida = 6 Then
            Try
                Dim conexion As New SQLite.SQLiteConnection()
                conexion.ConnectionString = cadena
                conexion.Open()
                cmd = conexion.CreateCommand
                cmd.CommandText = "DELETE FROM " & tabla & " WHERE id >=0"
                cmd.ExecuteNonQuery()
				
                'Refrescamos el control superior.
                DataGridView1.Refresh()
                Me.refrescar_Click(sender, New System.EventArgs())
				
                StatusStrip1.Text = "Estado: Se han eliminado todos los registros."
				
                ID.Text = ""
                empresa.Text = ""
            Catch ex As Exception
                MsgBox("Error en el borrado de registros de la base: " & ex.Message)
            End Try
        End If
    End Sub
	
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim conexion = New SQLiteConnection(cadena)
        Dim cmd = New SQLiteCommand("SELECT * FROM " & tabla, conexion)
        Dim sadapter = New SQLiteDataAdapter(cmd)
        Dim sTable As DataTable
        Dim sDs As DataSet
        sDs = New DataSet()
        sadapter.Fill(sDs, tabla)
        sTable = sDs.Tables(tabla)
        adapter.Update(sTable)
    End Sub
	
    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        ID.Text = ""
        empresa.Text = ""
		
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        If Not String.IsNullOrEmpty(DataGridView1.Item(0, i).Value.ToString) Then
            ID.Text = DataGridView1.Item(0, i).Value
        End If
        'El nombre
        If Not String.IsNullOrEmpty(DataGridView1.Item(1, i).Value.ToString) Then
            empresa.Text = DataGridView1.Item(1, i).Value
        End If
		
        'Necesario indicar mas campos en caso de diferente TABLA, ojo
		
        Me.Button6_Click(sender, New System.EventArgs())
    End Sub
	
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles buscar.Click
        DataGridView1.Refresh()
		
        Dim conexion As New SQLite.SQLiteConnection()
        Dim cmd As SQLiteCommand
        conexion.ConnectionString = cadena
        conexion.Open()
        cmd = conexion.CreateCommand
		
        Dim campo, valor As String
        campo = InputBox("Introduzca el nombre del campo tal y como aparece en la tabla:")
        If campo <> "" Then
            valor = InputBox("Introduzca el valor que desea buscar:")
            If valor <> "" Then

                If UCase(campo) = "ID" Then
                    cmd.CommandText = "Select * FROM " & tabla & " WHERE " & campo & " = " & valor & ""
                Else
                    cmd.CommandText = "Select * FROM " & tabla & " WHERE " & campo & " Like '%" & valor & "%'"
                End If
            End If
        End If
		
        Dim adapter = New SQLiteDataAdapter(cmd)
        Dim dt As DataTable = Nothing
        Dim ds As New DataSet
        DataGridView1.DataBindings.Clear()
		
        For i = 0 To Me.DataGridView1.Rows.Count - 1
            Me.DataGridView1.Rows(0).Selected = True
            Me.DataGridView1.Rows(0).Dispose()
            Me.DataGridView1.Rows.RemoveAt(Me.DataGridView1.SelectedRows(0).Index)
        Next
		
        DataGridView1.Refresh()
		
        Try
            adapter.Fill(DataSet1, tabla)
            adapter.Fill(ds, tabla)
            dt = ds.Tables(tabla)
        Catch ex As Exception
            MsgBox("Se ha cancelado la acción: " & ex.Message)
            Me.refrescar_Click(sender, New System.EventArgs())
        End Try
		
        'Haciendo un binding  ;-)... 
        bs.DataSource = adapter
		
        Me.DataGridView1.DataSource = DataSet1
        Me.DataGridView1.DataMember = tab
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        DataGridView1.DefaultCellStyle.BackColor = Color.Beige
    End Sub
	
    Private Sub Ncancel_Click(sender As Object, e As EventArgs) Handles Ncancel.Click
        Guardar.Enabled = False
        Modificar.Enabled = True
        Ncancel.Visible = False
        BNuevo.Enabled = True
        Borrar.Enabled = True
    End Sub
	
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Bseleccionados.Click
        ' Dim cnDb As OdbcConnection
        Dim salida, i As Integer
		
        Dim conexion As New SQLite.SQLiteConnection()
        'Dim Micomando As SQLiteCommand
        conexion.ConnectionString = cadena
        conexion.Open()
        cmd = conexion.CreateCommand
		
        If ID.Text <> "" Then
            salida = MsgBox("¿ Está seguro de realizar esta acción ?", MsgBoxStyle.YesNo)
        End If
		
        If salida = 6 Then
            Try
                For Each row As DataGridViewRow In DataGridView1.SelectedRows
                    cmd.CommandText = "DELETE FROM " & tabla & " WHERE id = " & row.Cells(0).Value
                    cmd.ExecuteNonQuery()
                Next
                estado.Text = "Estado: El registro/s se han eliminado correctamente."
                ID.Text = ""
                empresa.Text = ""
                'Refrescamos el control superior.
                DataGridView1.Refresh()
                Me.refrescar_Click(sender, New System.EventArgs())
            Catch ex As Exception
                MsgBox("Error en el borrado " & ex.Message)
            End Try
        End If
    End Sub
	
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form2.Show()
    End Sub
End Class