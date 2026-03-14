using Godot;

[GlobalClass]
public partial class Skill : Resource
{
    [Export] public Attack skill1 { get; set; }
    [Export] public Attack skill2 { get; set; }
}