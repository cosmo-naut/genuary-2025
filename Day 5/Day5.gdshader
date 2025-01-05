shader_type canvas_item;

void fragment()
{
	float PI = 3.142;
	float r = sin(TIME * .9);
	float g = sin(TIME * .8 + PI / 3.0);
	float b = sin(TIME * .7 + (PI / 3.0) * 2.0);
	
	COLOR.r = texture(TEXTURE, vec2(UV.x - 0.02 * r, UV.y)).r;
	COLOR.g = texture(TEXTURE, vec2(UV.x, UV.y + 0.02 * g)).g;
	COLOR.b = texture(TEXTURE, vec2(UV.x + 0.02 * b, UV.y)).b;
}