shader_type canvas_item;

void fragment()
{
	float PI = 3.14159;
	vec2 uvs = UV;
	uvs.x += sin(UV.y * PI) * 0.1 * sin(TIME * UV.y);
	uvs.y += cos(UV.x * PI) * 0.1 * cos(TIME * UV.x);
	
	uvs.x = mod(uvs.x, 1.0);
	uvs.y = mod(uvs.y, 1.0);
	
	COLOR = texture(TEXTURE, uvs);
}