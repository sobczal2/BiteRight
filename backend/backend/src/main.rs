use std::sync::Arc;
use anyhow::Error;
use crate::config::AppConfig;

mod config;
mod models;
mod handlers;
mod routes;
mod utils;
pub mod errors;

#[tokio::main]
async fn main() -> Result<(), Error> {
    let app_config = Arc::new(AppConfig::new()?);

    println!("{:?}", app_config);

    Ok(())
}
