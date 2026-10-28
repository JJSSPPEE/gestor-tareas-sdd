using GestorTareas.Core;

namespace GestorTareas;

// Formulario creado por código (sin Designer) para que sea fácil de copiar y entender.
public class MainForm : Form
{
    private readonly ServicioTareas _servicio = new();
    private readonly Label lblPendientes = new();
    private readonly TextBox txtTitulo = new();
    private readonly ComboBox cmbPrioridad = new();
    private readonly Button btnAgregar = new();
    private readonly Button btnCompletar = new();
    private readonly Button btnEliminar = new();
    private readonly DataGridView dgvTareas = new();

    public MainForm()
    {
        Text = "Gestor de Tareas";
        ClientSize = new Size(720, 480);
        StartPosition = FormStartPosition.CenterScreen;

        var lblTitulo = new Label { Text = "Título:", Left = 15, Top = 20, AutoSize = true };
        txtTitulo.SetBounds(70, 16, 250, 25);
        txtTitulo.MaxLength = ServicioTareas.LongitudMaximaTitulo;

        var lblPrioridad = new Label { Text = "Prioridad:", Left = 335, Top = 20, AutoSize = true };
        cmbPrioridad.SetBounds(405, 16, 100, 25);
        cmbPrioridad.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbPrioridad.Items.AddRange(Enum.GetValues<Prioridad>().Cast<object>().ToArray());
        cmbPrioridad.SelectedIndex = 1; // Media

        btnAgregar.Text = "Agregar";
        btnAgregar.SetBounds(520, 14, 90, 29);

        dgvTareas.SetBounds(15, 60, 690, 360);
        dgvTareas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvTareas.ReadOnly = true;
        dgvTareas.AllowUserToAddRows = false;
        dgvTareas.MultiSelect = false;
        dgvTareas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvTareas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        btnCompletar.Text = "Completar";
        btnCompletar.SetBounds(15, 435, 100, 30);
        btnCompletar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        btnEliminar.Text = "Eliminar";
        btnEliminar.SetBounds(125, 435, 100, 30);
        btnEliminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblPendientes.SetBounds(240, 442, 300, 20);
        lblPendientes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        Controls.AddRange(new Control[]
        {
            lblTitulo, txtTitulo, lblPrioridad, cmbPrioridad, btnAgregar,
            dgvTareas, btnCompletar, btnEliminar, lblPendientes
        });

        btnAgregar.Click += BtnAgregar_Click;
        btnCompletar.Click += BtnCompletar_Click;
        btnEliminar.Click += BtnEliminar_Click;

        Refrescar();
    }

    private void BtnAgregar_Click(object? sender, EventArgs e)
    {
        try
        {
            _servicio.Agregar(txtTitulo.Text, (Prioridad)cmbPrioridad.SelectedItem!);
            txtTitulo.Clear();
            txtTitulo.Focus();
            Refrescar();
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
       
    }

    private void BtnCompletar_Click(object? sender, EventArgs e)
    {
        var id = ObtenerIdSeleccionado();
        if (id == null) return;

        _servicio.Completar(id.Value);
        Refrescar();
    }

    private void BtnEliminar_Click(object? sender, EventArgs e)
    {
        var id = ObtenerIdSeleccionado();
        if (id == null) return;

        var confirmar = MessageBox.Show("¿Eliminar la tarea seleccionada?", "Confirmar",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirmar != DialogResult.Yes) return;

        _servicio.Eliminar(id.Value);
        Refrescar();
    }

    private int? ObtenerIdSeleccionado()
    {
        if (dgvTareas.CurrentRow == null)
        {
            MessageBox.Show("Seleccione una tarea de la lista.", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }
        return (int)dgvTareas.CurrentRow.Cells["Id"].Value;
    }

    private void Refrescar()
    {
        dgvTareas.DataSource = _servicio.Listar()
            .Select(t => new
            {
                t.Id,
                t.Titulo,
                Prioridad = t.Prioridad.ToString(),
                Estado = t.Completada ? "Completada" : "Pendiente"
            })
            .ToList();
        lblPendientes.Text = $"Pendientes: {_servicio.ContarPendientes()}";
    }
}
