shader_type canvas_item;

void fragment()
{
	vec2 uv = UV;
	uv.x = min(UV.x, 1.0 - UV.x);
	uv.y = min(UV.y, 1.0 - UV.y);
		
	COLOR = texture(TEXTURE, uv);
}