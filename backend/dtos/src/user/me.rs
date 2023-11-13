use serde::Serialize;

#[derive(Serialize)]
pub struct MeResponseDto {
    pub user_id: i32,
    pub name: String,
    pub email: String,
}