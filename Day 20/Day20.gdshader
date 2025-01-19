shader_type canvas_item;


void fragment() {
    vec2 uv = UV;
	
	vec2 center = vec2(0.5, 0.5);
	uv -= center;
	
	float strength = 0.0 + sin(TIME * 0.1) * 8.0;
	
	float rot = sin(distance(UV, center) * strength) + sin(distance(UV, center) * 2.0);
	
	float cos_a = cos(rot);
	float sin_a = sin(rot);
	mat2 rotation_matrix = mat2(vec2(cos_a, -sin_a), vec2(sin_a, cos_a));
	uv = rotation_matrix * uv;
	
	uv += center;

    COLOR = texture(TEXTURE, uv);
}
