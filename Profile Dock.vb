Public Class Profile_Dock

    Dim MoveBlocked As Boolean
    Dim AllBlocked As Boolean
    Dim ProfileMenu As Boolean
    Dim NumPad As Boolean
    Dim MappingMode As Boolean
    Dim Profile1Toggle As Boolean
    Dim Profile2Toggle As Boolean
    Dim Profile3Toggle As Boolean
    Dim Profile4Toggle As Boolean
    Dim Profile5Toggle As Boolean
    Dim Profile6Toggle As Boolean

    Private Sub ProfileName_TextChanged(sender As Object, e As EventArgs) Handles ProfileName.TextChanged
        If Profile1Toggle = True Then
            Profile1.Text = ProfileName.Text
        End If
        If Profile2Toggle = True Then
            Profile2.Text = ProfileName.Text
        End If
        If Profile3Toggle = True Then
            Profile3.Text = ProfileName.Text
        End If
        If Profile4Toggle = True Then
            Profile4.Text = ProfileName.Text
        End If
        If Profile5Toggle = True Then
            Profile5.Text = ProfileName.Text
        End If
        If Profile6Toggle = True Then
            Profile6.Text = ProfileName.Text
        End If
    End Sub
    'Private Function ShowNumPad() As Object
    '    Profile1.Visible = False
    '    Profile2.Visible = False
    '    Profile3.Visible = False
    '    Profile4.Visible = False
    '    Profile5.Visible = False
    '    Profile6.Visible = False
    '    SaveProfile.Visible = False
    '    LockAllButton.Visible = False
    '    UnlockButton.Visible = False
    '    ProfileName.Visible = False
    '    NumPad0.Visible = True
    '    NumPad1.Visible = True
    '    NumPad2.Visible = True
    '    NumPad3.Visible = True
    '    NumPad4.Visible = True
    '    NumPad5.Visible = True
    '    NumPad6.Visible = True
    '    NumPad7.Visible = True
    '    NumPad8.Visible = True
    '    NumPad9.Visible = True
    '    NumPadNumLock.Visible = True
    '    NumPadAdd.Visible = True
    '    NumPadDecimal.Visible = True
    '    NumPadDivide.Visible = True
    '    NumPadEnter.Visible = True
    '    NumPadSubtract.Visible = True
    '    NumPadMultiply.Visible = True
    'End Function
    'Private Function ShowProfile() As Object
    '    Profile1.Visible = True
    '    Profile2.Visible = True
    '    Profile3.Visible = True
    '    Profile4.Visible = True
    '    Profile5.Visible = True
    '    Profile6.Visible = True
    '    SaveProfile.Visible = True
    '    LockAllButton.Visible = True
    '    UnlockButton.Visible = True
    '    ProfileName.Visible = True
    '    NumPad0.Visible = False
    '    NumPad1.Visible = False
    '    NumPad2.Visible = False
    '    NumPad3.Visible = False
    '    NumPad4.Visible = False
    '    NumPad5.Visible = False
    '    NumPad6.Visible = False
    '    NumPad7.Visible = False
    '    NumPad8.Visible = False
    '    NumPad9.Visible = False
    '    NumPadNumLock.Visible = False
    '    NumPadAdd.Visible = False
    '    NumPadDecimal.Visible = False
    '    NumPadDivide.Visible = False
    '    NumPadEnter.Visible = False
    '    NumPadSubtract.Visible = False
    '    NumPadMultiply.Visible = False
    'End Function
    'Private Sub NumPadShow_MouseClick(sender As Object, e As EventArgs) Handles NumberPadToolStripMenuItem.Click
    '    NumPad = Not (NumPad)
    '    If NumPad = True Then
    '        Call Function() ShowNumPad()
    '    ElseIf NumPad = False Then
    '        Call Function() ShowProfile()
    '    End If
    'End Sub
    Function ButtonTestfor(ByRef buttonVar As Boolean, eventButton As Button, mouseEvent As MouseEventArgs) As Object
        If mouseEvent.Button = MouseButtons.Left Then
            If buttonVar = True Then
                eventButton.Cursor = Cursors.Default
                eventButton.BackColor = Color.FromArgb(51, 51, 51)
                eventButton.ForeColor = Color.FromArgb(221, 221, 221)
                buttonVar = False
            ElseIf buttonVar = False Then
                eventButton.Cursor = Cursors.No
                eventButton.BackColor = Color.Orange
                eventButton.ForeColor = Color.Black
                buttonVar = True
            End If
        End If
    End Function
    Private Sub Profile1_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If Profile1Toggle = True Then
                Profile1.Cursor = Cursors.Default
                Profile1.BackColor = Color.FromArgb(51, 51, 51)
                Profile1.ForeColor = Color.FromArgb(221, 221, 221)
                Profile1Toggle = False
            ElseIf Profile1Toggle = False Then
                Profile1.BackColor = Color.Orange
                Profile1.ForeColor = Color.Black
                Dim Profile2 As Boolean
                Dim Profile3 As Boolean
                Dim Profile4 As Boolean
                Dim Profile5 As Boolean
                Dim Profile6 As Boolean
                Profile2 = True
                Profile3 = True
                Profile4 = True
                Profile5 = True
                Profile6 = True
                Profile1Toggle = True
                If MappingMode = True Then
                    ProfileName.Text = Profile1.Text
                    ProfileName.ReadOnly = False
                    ProfileName.MaxLength = 16
                End If

            End If
        End If
    End Sub
    Private Sub Profile2_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If Profile2Toggle = True Then
                Profile2.Cursor = Cursors.Default
                Profile2.BackColor = Color.FromArgb(51, 51, 51)
                Profile2.ForeColor = Color.FromArgb(221, 221, 221)
                Profile2Toggle = False
            ElseIf Profile2Toggle = False Then
                Profile1Toggle = True
                Profile3Toggle = True
                Profile4Toggle = True
                Profile5Toggle = True
                Profile6Toggle = True
                Profile2.BackColor = Color.Orange
                Profile2.ForeColor = Color.Black
                Profile2Toggle = True
                If MappingMode = True Then
                    ProfileName.Text = Profile2.Text
                    ProfileName.ReadOnly = False
                    ProfileName.MaxLength = 16
                End If

            End If
        End If
    End Sub
    Private Sub Profile3_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If Profile3Toggle = True Then
                Profile3.Cursor = Cursors.Default
                Profile3.BackColor = Color.FromArgb(51, 51, 51)
                Profile3.ForeColor = Color.FromArgb(221, 221, 221)
                Profile3Toggle = False
            ElseIf Profile3Toggle = False Then
                Profile3.BackColor = Color.Orange
                Profile3.ForeColor = Color.Black
                Dim Profile1 As Boolean
                Dim Profile2 As Boolean
                Dim Profile4 As Boolean
                Dim Profile5 As Boolean
                Dim Profile6 As Boolean
                Profile1 = True
                Profile2 = True
                Profile4 = True
                Profile5 = True
                Profile6 = True
                Profile3Toggle = True
                If MappingMode = True Then
                    ProfileName.Text = Profile3.Text
                    ProfileName.ReadOnly = False
                    ProfileName.MaxLength = 16
                End If
            End If
        End If
    End Sub
    Private Sub Profile4_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If Profile4Toggle = True Then
                Profile4.Cursor = Cursors.Default
                Profile4.BackColor = Color.FromArgb(51, 51, 51)
                Profile4.ForeColor = Color.FromArgb(221, 221, 221)
                Profile4Toggle = False
            ElseIf Profile4Toggle = False Then
                Profile4.BackColor = Color.Orange
                Profile4.ForeColor = Color.Black
                Dim Profile1 As Boolean
                Dim Profile2 As Boolean
                Dim Profile3 As Boolean
                Dim Profile5 As Boolean
                Dim Profile6 As Boolean
                Profile1 = True
                Profile2 = True
                Profile3 = True
                Profile5 = True
                Profile6 = True
                Profile4Toggle = True
                If MappingMode = True Then
                    ProfileName.Text = Profile4.Text
                    ProfileName.ReadOnly = False
                    ProfileName.MaxLength = 16
                End If

            End If
        End If
    End Sub
    Private Sub Profile5_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If Profile5Toggle = True Then
                Profile5.Cursor = Cursors.Default
                Profile5.BackColor = Color.FromArgb(51, 51, 51)
                Profile5.ForeColor = Color.FromArgb(221, 221, 221)
                Profile5Toggle = False
            ElseIf Profile5Toggle = False Then
                Profile5.BackColor = Color.Orange
                Profile5.ForeColor = Color.Black
                Dim Profile1 As Boolean
                Dim Profile2 As Boolean
                Dim Profile3 As Boolean
                Dim Profile4 As Boolean
                Dim Profile6 As Boolean
                Profile1 = True
                Profile2 = True
                Profile3 = True
                Profile4 = True
                Profile6 = True
                Profile5Toggle = True
                If MappingMode = True Then
                    ProfileName.Text = Profile5.Text
                    ProfileName.ReadOnly = False
                    ProfileName.MaxLength = 16
                End If

            End If
        End If
    End Sub
    Private Sub Profile6_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If Profile6Toggle = True Then
                Profile6.Cursor = Cursors.Default
                Profile6.BackColor = Color.FromArgb(51, 51, 51)
                Profile6.ForeColor = Color.FromArgb(221, 221, 221)
                Profile6Toggle = False
            Else Profile6Toggle = False
                Profile6.BackColor = Color.Orange
                Profile6.ForeColor = Color.Black
                ProfileName.Text = Profile6.Text
                ProfileName.ReadOnly = False
                ProfileName.MaxLength = 16
                Dim Profile1 As Boolean
                Dim Profile2 As Boolean
                Dim Profile3 As Boolean
                Dim Profile4 As Boolean
                Dim Profile5 As Boolean
                Profile1 = True
                Profile2 = True
                Profile3 = True
                Profile4 = True
                Profile5 = True
                Profile6Toggle = True
            End If

        End If
    End Sub
    Private Sub SaveProfile_Click(sender As Object, e As EventArgs) Handles SaveProfile.Click
        SaveFileDialog1.ShowDialog()
    End Sub
End Class