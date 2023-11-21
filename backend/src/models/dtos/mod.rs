use crate::errors::api::ApiError;
use axum::extract::FromRequest;
use axum::http::{Request, StatusCode};
use axum::{async_trait, Json, RequestExt};
use serde_json::json;
use validator::Validate;

pub mod common;
mod refresh_token;
pub mod unit;
pub mod user;
pub mod category;
