use lazy_static::lazy_static;
use regex::Regex;

lazy_static! {
    pub static ref PRICE_REGEX: Regex = Regex::new(r"^[1-9]\d*(?:\.\d{2})$").expect("Invalid regex");
}